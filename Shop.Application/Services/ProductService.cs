using Shop.Application.Extensions;
using Shop.Application.Interfaces;
using Shop.Application.Utils;
using Shop.Domain.Interfaces;
using Shop.Domain.Models.Product;
using Shop.Domain.ViewModels.Product;
using Shop.Domain.ViewModels.ProductCategory;
using Shop.Domain.ViewModels.ProductDiscount;
using Shop.Domain.ViewModels.ProductGallery;

namespace Shop.Application.Services
{
    public class ProductService : IProductService
    {
        #region constructor

        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProductDiscountRepository _productDiscountRepository;
        private readonly IProductGalleryRepository _productGalleryRepository;

        public ProductService(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IProductDiscountRepository productDiscountRepository, IProductGalleryRepository productGalleryRepository)
        {
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
            _productDiscountRepository = productDiscountRepository;
            _productGalleryRepository = productGalleryRepository;
        }

        #endregion


        #region product

        public async Task<FilterProductViewModel> FilterProductInAdmin(FilterProductViewModel filter)
        {
            return await _productRepository.FilterProduct(filter);
        }

        public async Task<CreateProductResult> CreateProductInAdmin(CreateProductViewModel productViewModel)
        {
            Product product = new Product
            {
                Title = productViewModel.Title,
                Description = productViewModel.Description,
                Price = productViewModel.Price,
            };

            #region insert image

            if (productViewModel.ImageProduct != null && productViewModel.ImageProduct.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productViewModel.ImageProduct.FileName);
                productViewModel.ImageProduct.AddImageToServer(
                    imageName,
                    PathExtension.ProductImageOriginServer,
                    100,
                    100,
                    PathExtension.ProductImageThumbServer,
                null
                    );
                product.ImageName = imageName;
            }
            else
            {
                return CreateProductResult.NotValidImage;
            }

            #endregion

            await _productRepository.AddProduct(product);

            return CreateProductResult.Success;
        }

        public async Task<UpdateProductViewModel?> GetProductByIdForEditInAdmin(int productId)
        {
            var product = await _productRepository.GetProductById(productId);
            if (product == null) return null;

            return new UpdateProductViewModel
            {
                ProductId = product.Id,
                Description = product.Description,
                ImageName = product.ImageName,
                Price = product.Price,
                Title = product.Title,
            };
        }

        public async Task<UpdateProductResult> EditProductinAdmin(UpdateProductViewModel productViewModel)
        {
            var product = await _productRepository.GetProductById(productViewModel.ProductId);
            if (product == null) return UpdateProductResult.NotFoundProduct;

            product.Title = productViewModel.Title;
            product.Description = productViewModel.Description;
            product.Price = productViewModel.Price;

            #region insert image

            if (productViewModel.ImageProduct != null)
            {
                if (!productViewModel.ImageProduct.IsImage())
                    return UpdateProductResult.NotValidImage;

                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(productViewModel.ImageProduct.FileName);
                productViewModel.ImageProduct.AddImageToServer(
                    imageName,
                    PathExtension.ProductImageOriginServer,
                    100,
                    100,
                    PathExtension.ProductImageThumbServer,
                product.ImageName
                    );
                product.ImageName = imageName;
            }

            #endregion

            await _productRepository.UpdateProduct(product);
            return UpdateProductResult.Success;

        }

        public async Task<bool> DeleteProductByIdInAdmin(int productId)
        {
            var user = await _productRepository.GetProductById(productId);
            if (user == null || user.IsDelete) return false;
            user.IsDelete = true;
            await _productRepository.UpdateProduct(user);
            return true;
        }

        public async Task<string> GetProductTitleByProductId(int productId)
        {
            return await _productRepository.GetProductTitleByProductId(productId);
        }

        #endregion

        #region product category

        public async Task<FilterProductCategoryViewModel> FilterProductCategoryInAdmin(FilterProductCategoryViewModel filter)
        {
            return await _productCategoryRepository.FilterProductCategory(filter);
        }

        public async Task CreateProductCategoryInAdmin(CreateOrEditProductCategoryViewModel createProductCategory)
        {
            ProductCategory productCategory = new ProductCategory
            {
                Title = createProductCategory.Title,
                ParentId = createProductCategory.ParentId,
            };
            await _productCategoryRepository.AddProductCategory(productCategory);
        }

        public async Task<CreateOrEditProductCategoryViewModel?> GetProductCategoryByIdForEditinAdmin(int productCategoryId)
        {
            var productCategory = await _productCategoryRepository.GetProductCategoryById(productCategoryId);
            if (productCategory == null) return null;
            return new CreateOrEditProductCategoryViewModel
            {
                Title = productCategory.Title,
            };
        }

        public async Task<bool> UpdateProductCategoryInAdmin(CreateOrEditProductCategoryViewModel createProductCategory, int productCategoryId)
        {
            ProductCategory productCategory = await _productCategoryRepository.GetProductCategoryById(productCategoryId);
            if (productCategory == null) return false;
            productCategory.Title = createProductCategory.Title;
            await _productCategoryRepository.UpdateProductCategory(productCategory);
            return true;
        }

        public async Task<bool> DeleteProductCategoryInAdmin(int productCategoryId)
        {
            ProductCategory productCategory = await _productCategoryRepository.GetProductCategoryById(productCategoryId);
            if (productCategory == null || productCategory.IsDelete) return false;
            productCategory.IsDelete = true;
            await _productCategoryRepository.UpdateProductCategory(productCategory);
            return true;
        }

        public async Task<ProductCategory> GetProductCategoryById(int productCategoryId)
        {
            return await _productCategoryRepository.GetProductCategoryById(productCategoryId);
        }

        #endregion

        #region product discount

        public async Task<FilterProductDiscountViewModel> FilterProductDiscountInAdmin(FilterProductDiscountViewModel filter)
        {
            return await _productDiscountRepository.FilterProductDiscount(filter);
        }

        public async Task<CreateProductDiscountResult> CreateProductDiscountInAdmin(CreateProductDiscountViewModel createProductDiscount)
        {
            var productDiscount = await _productDiscountRepository.GetProductDiscountByDiscountCode(createProductDiscount.DiscountCode);
            if (productDiscount != null && !productDiscount.IsDelete) return CreateProductDiscountResult.ExistDiscount;

            var newProductDiscount = new ProductDiscount
            {
                DiscountCode = createProductDiscount.DiscountCode,
                Percentage = createProductDiscount.Percentage,
                StartTime = createProductDiscount.StartTime.ToMiladi(),
                EndTime = createProductDiscount.EndTime.ToMiladi()
            };
            await _productDiscountRepository.AddProductDiscount(newProductDiscount);
            return CreateProductDiscountResult.Success;
        }

        public async Task<UpdateProductDiscountViewModel?> GetProductDiscountForEditInAdmin(int productDiscountId)
        {
            var productDiscount = await _productDiscountRepository.GetProductDiscountById(productDiscountId);
            if (productDiscount == null || productDiscount.IsDelete) return null;
            return new UpdateProductDiscountViewModel
            {
                StartTime = productDiscount.StartTime.ToShamsiDateTime(),
                EndTime = productDiscount.EndTime.ToShamsiDateTime(),
                DiscountCode = productDiscount.DiscountCode,
                Percentage = productDiscount.Percentage,
            };
        }

        public async Task<bool> EditProductDiscountInAdmin(UpdateProductDiscountViewModel updateProductDiscount, int productDiscountId)
        {
            var productDiscount = await _productDiscountRepository.GetProductDiscountById(productDiscountId);
            if (productDiscount == null || productDiscount.IsDelete) return false;
            productDiscount.Percentage = updateProductDiscount.Percentage;
            productDiscount.StartTime = updateProductDiscount.StartTime.ToMiladi();
            productDiscount.EndTime = updateProductDiscount.EndTime.ToMiladi();

            await _productDiscountRepository.UpdateProductDiscount(productDiscount);
            return true;
        }

        public async Task<bool> DeleteProductDiscountInAdmin(int productDiscountId)
        {
            var productDiscount = await _productDiscountRepository.GetProductDiscountById(productDiscountId);
            if (productDiscount == null || productDiscount.IsDelete) return false;
            productDiscount.IsDelete = true;
            await _productDiscountRepository.UpdateProductDiscount(productDiscount);
            return true;
        }

        #endregion

        #region product gallery

        public async Task<ProductGalleryViewModel> GetProductGalleryByProductId(int productId)
        {
            var productGallery = await _productGalleryRepository.GetProductGalleryByProductId(productId);

            ProductGalleryViewModel galleryViewModels = new ProductGalleryViewModel();
            galleryViewModels.ProductTitle = await GetProductTitleByProductId(productId);
            galleryViewModels.ProductGalleries = productGallery;

            return galleryViewModels;
        }

        public async Task<CreateProductGalleryResult> CreateProductGallery(CreateProductGalleryViewModel createProductGallery)
        {
            var isExistProduct = await _productRepository.IsExistProductById(createProductGallery.ProductId);
            if (!isExistProduct) return CreateProductGalleryResult.NotFoundProduct;

            ProductGallery productGallery = new ProductGallery
            {
                ProductId = createProductGallery.ProductId,
            };

            #region insert image

            if (createProductGallery.ProductImage != null && createProductGallery.ProductImage.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(createProductGallery.ProductImage.FileName);
                createProductGallery.ProductImage.AddImageToServer(
                    imageName,
                    PathExtension.ProductImageOriginServer,
                    100,
                    100,
                    PathExtension.ProductImageThumbServer,
                null
                    );
                productGallery.ImageName = imageName;
            }
            else
            {
                return CreateProductGalleryResult.NotValidImage;
            }

            #endregion

            await _productGalleryRepository.AddProductGallery(productGallery);
            return CreateProductGalleryResult.Success;
        }

        public async Task<UpdateProductGalleryViewModel?> GetProductGalleryByIdForEdit(int productGalleryId)
        {
            var productGallery = await _productGalleryRepository.GetProductGalleryById(productGalleryId);
            if (productGallery == null) return null;

            return new UpdateProductGalleryViewModel
            {
                ProductId= productGallery.ProductId,
                ProductTitle = await _productRepository.GetProductTitleByProductId(productGallery.Id),
                ProductImageName = productGallery.ImageName,
            };
        }

        public async Task<UpdateProductGalleryResult> EditProductGallery(int productGalleryId, UpdateProductGalleryViewModel updateProductGallery)
        {
            var productGallery = await _productGalleryRepository.GetProductGalleryById(productGalleryId);
            if (productGallery == null) return UpdateProductGalleryResult.NotFoundProductGallery;

            #region insert image

            if (updateProductGallery.ProductImage != null && updateProductGallery.ProductImage.IsImage())
            {
                var imageName = Guid.NewGuid().ToString("N") + Path.GetExtension(updateProductGallery.ProductImage.FileName);
                updateProductGallery.ProductImage.AddImageToServer(
                    imageName,
                    PathExtension.ProductImageOriginServer,
                    100,
                    100,
                    PathExtension.ProductImageThumbServer,
                updateProductGallery.ProductImageName
                    );
                productGallery.ImageName = imageName;
            }
            else
            {
                return UpdateProductGalleryResult.NotValidImage;
            }

            #endregion

            await _productGalleryRepository.UpdateProductGallery(productGallery);
            return UpdateProductGalleryResult.Success;

        }

        public async Task<bool> DeleteProductGalleryById(int productGalleryId)
        {
            var productGallery = await _productGalleryRepository.GetProductGalleryById(productGalleryId);
            if (productGallery == null || productGallery.IsDelete) return false;
            productGallery.IsDelete = true;
            await _productGalleryRepository.UpdateProductGallery(productGallery);

            return true;
        }
        #endregion
    }
}
