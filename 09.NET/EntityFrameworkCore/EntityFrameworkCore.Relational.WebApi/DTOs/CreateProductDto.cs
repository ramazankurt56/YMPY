namespace EntityFrameworkCore.Relational.WebApi.DTOs
{
    public sealed record CreateProductDto(
        string ProductName,
        string ProductDescription,
        decimal ProductPrice,
        string CategoryName
        );
        //public CreateProductDto(string productName,string productDescription,decimal productPrice, string categoryName)
        //{
        //    ProductName = productName;
        //    ProductDescription = productDescription;
        //    ProductPrice = productPrice;
        //    CategoryName = categoryName;
        //}
        //public string ProductName { get; set; }
        //public string ProductDescription { get; set; }  
        //public decimal ProductPrice { get; set; } 
        //public string CategoryName { get; set; }
    
}
