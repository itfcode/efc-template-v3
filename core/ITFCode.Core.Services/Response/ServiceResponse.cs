namespace ITFCode.Core.Services.Response
{
    public abstract class ServiceResponse<TValue>
    {
        #region Private & Protected 

        private readonly List<string> _errors;

        private readonly TValue? _value;

        #endregion

        #region Public Properties 

        public bool Succeeded => _errors.Count == 0;

        public IEnumerable<string> Errors => _errors;

        public TValue? Value => _value;

        #endregion

        #region Constructors 

        public ServiceResponse(TValue value) : this(value, Enumerable.Empty<string>()) { }

        public ServiceResponse(IEnumerable<string> errors) : this(default, errors) { }

        public ServiceResponse(TValue? value, IEnumerable<string> errors)
        {
            _value = value;
            _errors = new List<string>(errors ?? Enumerable.Empty<string>());
        }

        #endregion
    }
}
