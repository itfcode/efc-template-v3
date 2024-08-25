using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ITFCode.Core.Services.Identity.Base
{
    public abstract class IdentityBaseService<TIdentity, TIdentityDTO>
    {
        #region Private & Protected Fileds

        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        #endregion

        #region Protected Properties 

        protected ILogger Logger => _logger ?? throw new NullReferenceException("ApplicationRoleService: Logger not defined");
        protected IMapper Mapper => _mapper ?? throw new NullReferenceException("ApplicationRoleService: Mapper not defined");

        #endregion

        #region Constructors 

        public IdentityBaseService(ILogger<IdentityBaseService<TIdentity, TIdentityDTO>> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        #endregion

        #region Private Methods

        protected TIdentity Map(TIdentityDTO dto) => Mapper.Map<TIdentity>(dto);

        protected TIdentityDTO Map(TIdentity entity) => Mapper.Map<TIdentityDTO>(entity);

        #endregion
    }
}