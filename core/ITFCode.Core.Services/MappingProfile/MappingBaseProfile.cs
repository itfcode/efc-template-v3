namespace ITFCode.Core.Services.MappingProfile
{
    public abstract class MappingBaseProfile<TEntity, TEntityDTO> : AutoMapper.Profile
        where TEntity : class
        where TEntityDTO : class
    {
        #region Constructors 

        public MappingBaseProfile()
        {
            ConfigureMap();
        }

        #endregion

        #region Protected Methods

        protected virtual void ConfigureMap()
        {
            CreateMap<TEntity, TEntityDTO>().ReverseMap();
        }

        #endregion
    }
}