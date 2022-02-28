using Xunit;

namespace TodoScreenTaskAutomation.Configure
{
    // essa annotation possibilita abrir o browser apenas uma vez para realizar todos os testes de uma mesma classe
    [CollectionDefinition("Chrome Driver")]
    public class CollectionFixture : ICollectionFixture<TestFixture>
    {
        
    }
}