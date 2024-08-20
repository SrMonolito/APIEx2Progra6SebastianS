namespace APIExamen2Progra6SebastianSancho.ModelsDTOs
{
    public class UserDTO
    {
        public int UsuarioID { get; set; }

        public string? UsuarioEmail { get; set; }

        public string? UsuarioName { get; set; }

        public string? UsuarioLastName { get; set; }

        public string? UsuarioPhone { get; set; }

        public string? UsuarioPassword { get; set; }

        public int? UsuarioStrikeCount { get; set; }

        public string? UsuarioBackUpEmail { get; set; }

        public string? UsuarioJobDescription { get; set; }

        public int? UsuarioEstadoID { get; set; }

        public int? UsuarioPaisID { get; set; }

        public int? UsuarioRolID { get; set; }
    }
}
