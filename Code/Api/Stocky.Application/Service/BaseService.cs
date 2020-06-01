namespace Stocky.Application.Service
{
    public class BaseService
    {
        protected IBaseServiceInjector _injector;
        public BaseService(IBaseServiceInjector injector)
        {
            _injector = injector;
        }
    }
}
