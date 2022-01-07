namespace Reviews
{
    //{
    //    reviews {
    //        id authorId upc body
    //    }
    //    reviewsByAuthor(authorId: 1) {
    //       id authorId upc body
    //    }
    //    reviewsByProduct(upc: 1) {
    //        id authorId upc body
    //    }
    //}

    public class Query
    {
        public IEnumerable<Review> GetReviews([Service] ReviewRepository repository)
        {
            return repository.GetReviews();
        }

        public IEnumerable<Review> GetReviewsByAuthor([Service] ReviewRepository repository, int authorId)
        {
            return repository.GetReviewsByAuthorId(authorId);
        }

        public IEnumerable<Review> GetReviewsByProduct([Service] ReviewRepository repository, int upc)
        {
            return repository.GetReviewsByAuthorId(upc);
        }
    }
}
