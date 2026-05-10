namespace LowLevelDotNET.Infrastructure.DI
{
    public class Container
    {
        //Criar coleção chave => valor para fazer mapeamento
        private readonly Dictionary<Type, Type> _map = new(); 

        //Criar método que adiciona items a essa coleção ()
        public void Register<TInterface, TImplementation>()
        {
            _map[typeof(TInterface)] = typeof(TImplementation);
        }

        // Método que converte
        public Object Resolve(Type type)
        {
            //Verifica se está na coleção
            if (_map.ContainsKey(type))
            {
                type = _map[type];
            }

            //pega o construtor
            var construtor = type.GetConstructors().First();

            //Pega os parâmetros, instancia os parâmetros
            var parameters = construtor.GetParameters()
            .Select(p => Resolve(p.ParameterType))
            .ToArray();
            
            //Aqui ele cria o objeto 
            return Activator.CreateInstance(type, parameters);
        }

        //Não entendi esse aqui
        public T Resolve<T>()
        {
            return (T) Resolve(typeof(T));
        }
    }
}
