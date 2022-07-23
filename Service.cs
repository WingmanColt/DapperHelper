 public async Task<OperationResult> CRUD(CreateJobInputModel viewModel, User user, CrudActionEnum action)
        {
            var jobs = new Jobs();
            // jobs.Update(viewModel, ApproveType.Waiting, user);
            jobs.StatementType = action.GetDisplayName();

            using (IDbConnection connection = Connection)
            {
                var parameters = DapperPropertiesHelper.AutoParameterFind(jobs);

                if (action.Equals(JobCrudActionEnum.Create))
                    parameters.Add("Id", 0);
                else
                    parameters.Add("Id", viewModel.Id);
                        
                try
                {
                    connection.Open();
                    await connection.ExecuteAsync("spJobCRUD", parameters, commandType: CommandType.StoredProcedure);
                    connection.Close();

                    return OperationResult.SuccessResult("");
                }
                catch (Exception ex)
                {
                   return OperationResult.FailureResult(ex.Message);
                }
            }

        }
    public enum CrudActionEnum : int
    {
        None = 0,
        
        [Display(Name = "Create")]
        Create = 1,
        [Display(Name = "Update")]
        Update = 2,
        [Display(Name = "Delete")]
        Delete = 3,
        [Display(Name = "GetAll")]
        GetAll = 4
    }
