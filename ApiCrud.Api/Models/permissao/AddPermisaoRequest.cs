using System;

namespace ApiCrud.Permisao
{
    public record AddPermissaoRequest(
        string Permissaotipo,
        int Usuarioid,
        int Documentoid
    );
}
