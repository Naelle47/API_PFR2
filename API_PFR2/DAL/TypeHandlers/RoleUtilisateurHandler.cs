using API_PFR2.Domain.Enums;
using Dapper;
using System.Data;
namespace API_PFR2.DAL.TypeHandlers;

public class RoleUtilisateurHandler : SqlMapper.TypeHandler<RoleUtilisateur>
{
    public override RoleUtilisateur Parse(object value)
    {
        return Enum.Parse<RoleUtilisateur>(value.ToString()!);
    }

    public override void SetValue(IDbDataParameter parameter, RoleUtilisateur value)
    {
        parameter.Value = value.ToString();
    }
}