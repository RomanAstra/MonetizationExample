using System.Collections;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.UI;

namespace IAP
{
    internal class IAPManager : MonoBehaviour, IStoreListener
    {
        private IStoreController _controller;
        private IExtensionProvider _extensions;
        [SerializeField] private Button _buyButton;
        private string _productId;

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extensions = extensions;

            foreach (Product product in controller.products.all)
                Debug.Log($"{product.metadata.localizedTitle}");
        }

        public void OnInitializeFailed(InitializationFailureReason error) // инициализация не прошла
        {
            Debug.LogError($"{error}");
        }

        public void OnPurchaseFailed(Product i, PurchaseFailureReason p)  // покупка не прошла
        {
            Debug.LogError($"{p}");
        }

        IEnumerator DoPurchase(Product product)
        {
            yield return new WaitForSeconds(1.0f);
            _controller.ConfirmPendingPurchase(product);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
        {
            Product product = e.purchasedProduct;
            Debug.Log($"{product.metadata.localizedTitle}");

          #if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
            var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
                AppleTangle.Data(), Application.identifier);

            try {
                var result = validator.Validate(e.purchasedProduct.receipt);

                Debug.Log("Receipt is valid. Contents:");
                foreach (IPurchaseReceipt productReceipt in result) {
                    Debug.Log(productReceipt.productID);
                    Debug.Log(productReceipt.purchaseDate);
                    Debug.Log(productReceipt.transactionID);
                }
            } catch (IAPSecurityException) {
                Debug.Log("Invalid receipt, not unlocking content");
                return PurchaseProcessingResult.Complete;
            }
          #endif

            StartCoroutine(DoPurchase(product));
            return PurchaseProcessingResult.Pending;
        }

        private void Start()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            IAPConfigurationHelper.PopulateConfigurationBuilder(ref builder, ProductCatalog.LoadDefaultCatalog());
            _productId = "disable_ads";
            builder.AddProduct(_productId, ProductType.NonConsumable);
            UnityPurchasing.Initialize(this, builder);
        }

        private void OnEnable()
        {
            _buyButton.onClick.AddListener(PurchaseNoAds);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(PurchaseNoAds);
        }

        private void PurchaseNoAds()
        {
            _controller.InitiatePurchase(_productId);
        }

        public void RestorePurchases()
        {
             // IAPButton
            // iOS only
            _extensions.GetExtension<IAppleExtensions>().RestoreTransactions((bool success) => { });
        }
    }
}
