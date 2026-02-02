using AutoMapper;
using BusinessManagementAPI.Data;

namespace BusinessManagementAPI.UseCases.Base;

public class BaseHandler
{
    public SQLContext m_Context = default!;
    public IMapper m_Mapper = default!;

    public BaseHandler(IMapper mapper, SQLContext context)
    {
        m_Mapper = mapper;
        m_Context = context;
    }
}
