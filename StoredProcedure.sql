ALTER PROCEDURE [dbo].[spJobCRUD](
@id int,
@Name nvarchar(50), 
@LocationId nvarchar(20),
@ExprienceLevels INTEGER = 0,
@JobType int = 0,
@Description nvarchar(3000),
@MinSalary int = NULL,
@MaxSalary int = NULL,
@SalaryType int = 0,
@Promotion int = 0,
@PremiumPackage int = 0, 
@CategoryId int = 0,
@CompanyId int = 0,
@Adress nvarchar(30),
@CompanyLogo varchar(100) = NULL,
@LanguageId nvarchar(200) = NULL,
@TagsId nvarchar(200) = NULL,
@isArchived bit,
@isApproved int = 0,
@WorkType nvarchar(200),
@Rating decimal(10,2),
@RatingVotes int = 0,
@VotedUsers int = 0,
@Views int = 0,
@ApplyCount int = 0,
@PosterId varchar(60),
@CreatedOn datetime,
@ExpiredOn datetime,
@StatementType NVARCHAR(20) = '')
AS
BEGIN
SET NOCOUNT ON;

      IF @StatementType = 'Create'
       BEGIN 
            INSERT INTO [db_a75515_test].[dbo].[Jobs](
			 Name,
             LocationId,
             ExprienceLevels,
             JobType,
             Description,
			 MinSalary,
			 MaxSalary,
			 SalaryType,
			 Promotion,
			 PremiumPackage,
			 CategoryId,
			 CompanyId,
			 Adress,
			 CompanyLogo,
			 LanguageId,
			 TagsId,
			 isArchived,
			 isApproved,
			 WorkType,
			 Rating,
			 RatingVotes,
			 VotedUsers,
			 Views,
			 PosterId,
			 CreatedOn,
             ExpiredOn,
			 ApplyCount)
            VALUES      
			(@Name,
             @LocationId,
             @ExprienceLevels,
             @JobType,
             @Description,
			 @MinSalary,
			 @MaxSalary,
			 @SalaryType,
			 @Promotion,
			 @PremiumPackage,
			 @CategoryId,
			 @CompanyId,
			 @Adress,
			 @CompanyLogo,
			 @LanguageId,
			 @TagsId,
			 @isArchived,
			 @isApproved,
			 @WorkType,
			 @Rating,
			 @RatingVotes,
			 @VotedUsers,
			 @Views,
			 @PosterId,
			 @CreatedOn,
             @ExpiredOn,
			 @ApplyCount)	 
               END 



   IF @StatementType = 'Select'
        BEGIN
            SELECT * FROM [db_a75515_test].[dbo].[Jobs]
        END

IF @StatementType = 'Update'
        BEGIN
            UPDATE [db_a75515_test].[dbo].[Jobs]
                     SET Name = @Name,
             LocationId = @LocationId,
             ExprienceLevels = @ExprienceLevels,
             JobType = @ExprienceLevels,
             Description = @Description,
				MaxSalary = @MaxSalary,
			 MinSalary = @MinSalary,
			 SalaryType = @SalaryType,
			 Promotion = @Promotion,
			 PremiumPackage = @PremiumPackage,
			 CategoryId = @CategoryId,
			 CompanyId = @CompanyId,
			 Adress = @Adress,
			 CompanyLogo = @CompanyLogo,
			 LanguageId = @LanguageId,
			 TagsId = @TagsId,
			 isArchived = @isArchived,
			 isApproved = @isApproved,
			 WorkType = @WorkType
			 WHERE Id = @id
        END
      ELSE IF @StatementType = 'Delete'
        BEGIN
            DELETE FROM [db_a75515_test].[dbo].[Jobs]
            WHERE  id = @id
        END
END


