using MyWebApi.Core.Model;

namespace MyWebApi.Core.Dto
{
    public class StuffMapping
    {
        public DyeStuffs StuffSaveMap(StuffPostRequest request,int userId)
        {
            if (request == null)
                return null;

            return new DyeStuffs
            {
                Dyestuffs=request.Dyestuffs,
                IsActive = request.IsActive,
                CreatedBy = userId, 
                CreatedDate = DateTime.UtcNow
            };
        }
        public DyeStuffs StuffUpdateMap(DyeStuffs entity, StuffPostRequest request,int userId)
        {
            if (entity == null || request == null)
                return entity;

            entity.Dyestuffs = request.Dyestuffs?.Trim();
            entity.UpdatedBy = userId;
            entity.UpdatedDate = DateTime.UtcNow;
            entity.IsActive = request.IsActive;

            return entity;
        }

        public DyeTypes DyeTypeSaveMap(DyeTypePostRequest request, int userId)
        {
            if (request == null)
                return null;

            return new DyeTypes
            {
                DyeTypeName = request.DyeTypeName,
               DyeTypeCode=request.DyeTypeCode,
                IsActive = request.IsActive,
                CreatedBy = userId,
                CreatedOn = DateTime.UtcNow
            };
        }
        public DyeTypes DyeTypeUpdateMap(DyeTypes entity, DyeTypePostRequest request, int userId)
        {
            if (entity == null || request == null)
                return entity;

            entity.DyeTypeName = request.DyeTypeName?.Trim();
            entity.UpdatedBy = userId;
            entity.UpdatedOn= DateTime.UtcNow;
            entity.IsActive = request.IsActive;

            return entity;
        }
    }
}
