namespace PagoServicios
{

#pragma warning disable CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
    public class Configuration


    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public Logging Logging { get; set; }

        public Connectionstrings ConnectionStrings { get; set; }
            public string AllowedHosts { get; set; }
        }

        public class Logging
        {
            public Loglevel LogLevel { get; set; }
        }

        public class Loglevel
        {
            public string Default { get; set; }
            public string MicrosoftAspNetCore { get; set; }
        }

        public class Connectionstrings
        {
            public string DefaultConnection { get; set; }
        }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
#pragma warning restore CS1591 // Falta el comentario XML para el tipo o miembro visible públicamente
}
