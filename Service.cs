 public async Task<OperationResult> CRUD(object parameters, JobCrudActionEnum action, bool AutoFindParams, string skipAttribute, User user)
        {
            var param = new DynamicParameters();

            if (AutoFindParams)
                param = DapperPropertiesHelper.AutoParameterFind(parameters, skipAttribute);
            else
                param.AddDynamicParams(parameters);

            var model = new { StatementType = action.GetDisplayName() };
            param.AddDynamicParams(model);


            if (action.Equals(JobCrudActionEnum.Create) || action.Equals(JobCrudActionEnum.Update))
            {
                Create jobs = new Create();
                jobs.Update((CreateJobInputModel)parameters, ApproveType.Waiting, user);

                param.Add("@newId", dbType: DbType.Int32, size: 50, direction: ParameterDirection.Output);
            }

            if (action.Equals(JobCrudActionEnum.Create))
                param.Add("Id", 0);

            using (IDbConnection connection = Connection)
            {
                try
                {
                    connection.Open();
                    var result = await connection.ExecuteScalarAsync<int>("spJobCRUD", param, commandType: CommandType.StoredProcedure);
                    connection.Close();

                    if (action.Equals(JobCrudActionEnum.Create) || action.Equals(JobCrudActionEnum.Update))
                    {
                        OperationResult.SuccessResult("").Id = param.Get<int?>("@newId"); 
                    }
                    return OperationResult.SuccessResult("");
                }
                catch (Exception ex)
                {
                   return OperationResult.FailureResult(ex.Message);
                }
            }

        }

        
        public async Task<IAsyncEnumerable<T>> GetAll<T>(object parameters, JobGetActionEnum state, bool AutoFindParams, string skipAttribute)
        {
            var param = new DynamicParameters();

            if(AutoFindParams)
                param = DapperPropertiesHelper.AutoParameterFind(parameters, skipAttribute);
            else
                param.AddDynamicParams(parameters);

            var model = new { StatementType = state.GetDisplayName(), Id = 0 };
            param.AddDynamicParams(model);
            
            using (IDbConnection connection = Connection)
            {
                connection.Open();
                var result = await connection.QueryAsync<T>("spJobCRUD", param, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                connection.Close();

                return result.ToAsyncEnumerable();
            }
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            using (IDbConnection connection = Connection)
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<T>("spJobCRUD", new { Id = id > 0 ? id : 0, StatementType = "Select" }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                connection.Close();

                return result;
            }
        }

        public async Task<int> GetAllCountBy(object parameters)
        {
            var param = new DynamicParameters();
            var model = new { StatementType = "GetAllCountBy", Id = 0 };
            param.AddDynamicParams(model);
            param.AddDynamicParams(parameters);

            using (IDbConnection connection = Connection)
            {
                connection.Open();
                var result = await connection.ExecuteScalarAsync<int>("spJobCRUD", param, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                connection.Close();

                return result;
            }
        }

        public async Task<bool> AddRatingToJobs(object parameters)
        {
            var param = new DynamicParameters();
            var model = new { StatementType = "AddRatingToJobs" };
            param.AddDynamicParams(model);
            param.AddDynamicParams(parameters);

            using (IDbConnection connection = Connection)
            {
                connection.Open();
                var result = await connection.ExecuteAsync("spJobCRUD", param, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                connection.Close();

                return result > 0;
            }
        }
public enum JobCrudActionEnum : int
    {
        None = 0,

        [Display(Name = "Create")]
        Create = 1,

        [Display(Name = "Update")]
        Update = 2,

        [Display(Name = "Delete")]
        Delete = 3,

        [Display(Name = "UpdatePromotion")]
        UpdatePromotion = 4,

        [Display(Name = "RefreshDate")]
        RefreshDate = 5,

        [Display(Name = "UpdateUser")]
        UpdateUser = 6
    }

    public enum JobGetActionEnum : int
    {
        None = 0,

        [Display(Name = "GetAllFiltering")]
        GetAllFiltering = 1,

        [Display(Name = "GetAllBy")]
        GetAllBy = 2,

        [Display(Name = "GetAllForDashboard")]
        GetAllForDashboard = 3
    }
