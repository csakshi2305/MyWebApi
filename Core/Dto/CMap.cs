using MyWebApi.Core.Dto;
using MyWebApi.Core.Model;

public class CMap
{
    //company map

    public Company CompanySaveMap(PostRequest request)
    {
        if (request == null)
            return null;

        return new Company
        {
            CompanyName = request.CompanyName?.Trim(),
            DepartmentId = request.DepartmentId,
            IsActive = request.IsActive
        };
    }
    public Company CompanyUpdateMap(Company entity, PostRequest request)
    {
        if (entity == null || request == null)
            return entity;

       entity.CompanyName = request.CompanyName?.Trim();
        entity.DepartmentId = request.DepartmentId;
        entity.IsActive = request.IsActive;

        return entity;
    }
    public  Department MapToEntity(DeleteRequestDep dto)
    {
        return new Department
        {
            Id = dto.Id,
            IsActive = dto.IsActive
        };
    }

    //fabric map

    public FabricTypeMaster FabriceSaveMap(FabricPostRequest fabricPostRequest)
    {
        if (fabricPostRequest == null)
            return null;

        return new FabricTypeMaster
        {
            Fabric_Type = fabricPostRequest.Fabric_Type?.Trim(),
           // Construction_Id=fabricPostRequest.Construction_Id,
         
            IsActive = fabricPostRequest.IsActive
        };
    }

    public FabricTypeMaster FabricUpdateMap(FabricTypeMaster entity, FabricPostRequest request)
    {
        if (entity == null || request == null)
            return entity;

        entity.Fabric_Type = request.Fabric_Type?.Trim();
     //   entity.Construction_Id = request.Construction_Id;
        entity.IsActive = request.IsActive;
     
        return entity;
    }

   

    //construction map

    public Construction ConstructionSaveMap(ConstructionPostRequest constructionPostRequest)
    {
        if (constructionPostRequest == null)
            return null;

        return new Construction
        {

     
             Construction_Id= constructionPostRequest.Construction_Id,
             Fabric_id=constructionPostRequest.Fabric_id,
             Construction_Type= constructionPostRequest.Construction_Type
             


        };
    }

    public Construction ConstructionUpdateMap(Construction entity, ConstructionPostRequest request)
    {
        if (entity == null || request == null)
            return entity;

        entity.Construction_Id=request.Construction_Id;
        entity.Construction_Type = request.Construction_Type;
        entity.Fabric_id = request.Fabric_id;
        
        
        return entity;
    }

    //category map

    public Symbol_Category_Master CategorySaveMap(CategoryPostRequest categoryPostRequest)
    {
        if (categoryPostRequest == null)
            return null;

        return new Symbol_Category_Master
        {
           Symbol_Category_Name = categoryPostRequest.Symbol_Category_Name?.Trim(),
            IsActive = categoryPostRequest.IsActive
        };
    }

    public Symbol_Category_Master CategoryUpdateMap(Symbol_Category_Master entity, CategoryPostRequest request)
    {
        if (entity == null || request == null)
            return entity;

        entity.Symbol_Category_Name = request.Symbol_Category_Name?.Trim();
        entity.IsActive = request.IsActive;

        return entity;
    }

    // individualsymbol

    public IndividualCareSymbol IndividualSaveMap(IndividualPostRequests IndividualPostRequest)
    {
        if (IndividualPostRequest == null)
            return null;

        return new IndividualCareSymbol
        {
            Name = IndividualPostRequest.Name?.Trim(),
            ImagePathURL=IndividualPostRequest.ImagePathURL,
            UniqueId=IndividualPostRequest.UniqueId,
            imageName=IndividualPostRequest.imageName,
            Description=IndividualPostRequest.Description,
            Symbol_Category_id=IndividualPostRequest.Symbol_Category_id,
            CountryId=IndividualPostRequest.CountryId,
            IsActive = IndividualPostRequest.IsActive
        };
    }

    public IndividualCareSymbol IndividualUpdateMap(IndividualCareSymbol entity, IndividualPostRequests request)
    {
        if (entity == null || request == null)
            return entity;

        entity.Name = request.Name?.Trim();
        entity.ImagePathURL = request.ImagePathURL;
        entity.UniqueId = request.UniqueId;
        entity.imageName = request.imageName;
        entity.Description = request.Description;
        entity.Symbol_Category_id = request.Symbol_Category_id;
        entity.CountryId = request.CountryId;
        entity.IsActive = request.IsActive;


        return entity;
    }

    //country map


    public Country CountrySaveMap(CountryPostRequests countryPostRequests)
    {
        if (countryPostRequests == null)
            return null;

        return new Country
        {
            CountryName = countryPostRequests.CountryName?.Trim(),
            IsActive = countryPostRequests.IsActive
        };
    }

    public Country  CountryUpdateMap(Country entity, CountryPostRequests request)
    {
        if (entity == null || request == null)
            return entity;

        entity.CountryName = request.CountryName?.Trim();
        entity.IsActive = request.IsActive;

        return entity;
    }

}
