﻿using System;
using System.Collections.Generic;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Http.Features.Internal;
using Microsoft.AspNet.Http.Internal;
using Microsoft.Framework.Primitives;
using Xunit;

namespace MusicStore.Models
{
    public class ShoppingCartTest
    {
        [Fact]
        public void GetCartId_ReturnsCartIdFromCookies()
        {
            // Arrange
            var cartId = "cartId_A";

            var httpContext = new DefaultHttpContext();
            httpContext.Features.Set<IRequestCookiesFeature>(new CookiesFeature("Session", cartId));

            var cart = new ShoppingCart(new MusicStoreContext());

            // Act
            var result = cart.GetCartId(httpContext);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cartId, result);
        }

        private class CookiesFeature : IRequestCookiesFeature
        {
            private IReadableStringCollection _cookies;

            public CookiesFeature(string key, string value)
            {
                _cookies = new ReadableStringCollection(new Dictionary<string, StringValues>()
                {
                    { key, new[] { value } }
                });
            }

            public IReadableStringCollection Cookies
            {
                get { return _cookies; }
                set { _cookies = value; }
            }
        }
    }
}
