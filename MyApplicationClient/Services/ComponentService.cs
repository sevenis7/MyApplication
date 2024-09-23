using MyApplicationDataLayer.Entities;
using System.Net.Http.Json;

namespace MyApplicationClient.Services
{
    public class ComponentService
    {
        private readonly HttpClient _httpClient;
        private IEnumerable<Component> _components = Enumerable.Empty<Component>();

        public ComponentService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }

        public async Task InitializeAsync()
        {
            Console.Out.WriteLine($"Initializing _components with hashcode - {_components.GetHashCode()}");

            var response = await _httpClient.GetFromJsonAsync<IEnumerable<Component>>($"api/component");

            _components = response ?? Enumerable.Empty<Component>();

            foreach (var e in _components)
            {
                Console.Out.WriteLine($"Component Name: {e.Name}, Value: {e.Value} hashcode - {_components.GetHashCode()}");
            }
        }

        public async Task<string?> Get(string name)
        {
            Console.Out.WriteLine($"Getting informantion from _components with hashcode - {_components.GetHashCode()}");

            Console.Out.WriteLine($"Searching for component with name: {name}");

            if (!_components.Any())
            {
                Console.Out.WriteLine($"_components with hashcode - {_components.GetHashCode()} is empty");
                return null;
            }
            else
            {
                foreach (var e in _components)
                {
                    Console.Out.WriteLine($"Component Name: {e.Name}, Value: {e.Value}");
                }

                var component = _components.FirstOrDefault(x => x.Name == name);

                if (component == null)
                {
                    Console.Out.WriteLine($"Component with name {name} not found.");
                    throw new InvalidOperationException($"Component with name {name} not found.");
                }

                return component.Value;
            }

        }
    }
}
