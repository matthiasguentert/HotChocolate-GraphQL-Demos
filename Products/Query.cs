namespace Products
{
    public class Query
    {
        public IEnumerable<Product> GetTopProducts(int first, [Service] ProductRepository repository)
        {
            return repository.GetTopProducts(first);
        }

        public Product GetProduct(int upc, [Service] ProductRepository repository)
        {
            return repository.GetProduct(upc);
        }
    }
}
