using AutoMapper;
using InvoiceAutomationAPI.Data;

namespace InvoiceAutomationAPI.UseCases.Base
{

    public class BaseHandler
    {
        public SQLContext m_Context = new();
        public IMapper m_Mapper = default!;

        public BaseHandler(IMapper mapper)
            => m_Mapper = mapper;
    }

}
