SET IDENTITY_INSERT [dbo].[Core_AppSetting] ON 
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (1, N'Catalog.ProductPageSize', N'10', 1, N'Catalog')
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (2, N'Global.AssetVersion', N'1.0', 1, N'Core')
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (3, N'News.PageSize', N'10', 1, N'News')
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (4, N'GoogleAppKey', N'AIzaSyBmsQV2FUo6g52R1kovLyfvaYm4FryNs4g', 0, N'Core')
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (5, N'SmtpServer', N'smtp.gmail.com', 0, N'Core')
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (6, N'SmtpPort', N'587', 0, N'Core')
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (7, N'SmtpUsername', N'', 0, N'Core')
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (8, N'SmtpPassword', N'', 0, N'Core')
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (9, N'Theme', N'Generic', 0, N'Core')
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (10, N'Tax.IsProductPriceIncludeTax', N'true', 1, N'Tax')
INSERT [dbo].[Core_AppSetting] ([Id], [Key], [Value], [IsVisibleInCommonSettingPage], [Module]) VALUES (11, N'Tax.DefaultTaxClassId', N'1', 1, N'Tax')
SET IDENTITY_INSERT [dbo].[Core_AppSetting] OFF
GO

SET IDENTITY_INSERT [dbo].[Core_Role] ON 
INSERT [dbo].[Core_Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (1, N'admin', N'ADMIN', N'bd3bee0b-5f1d-482d-b890-ffdc01915da3')
INSERT [dbo].[Core_Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (2, N'customer', N'CUSTOMER', N'bd3bee0b-5f1d-482d-b890-ffdc01915da3')
INSERT [dbo].[Core_Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (3, N'guest', N'GUEST', N'bd3bee0b-5f1d-482d-b890-ffdc01915da3')
INSERT [dbo].[Core_Role] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (4, N'vendor', N'VENDOR', N'bd3bee0b-5f1d-482d-b890-ffdc01915da3')
SET IDENTITY_INSERT [dbo].[Core_Role] OFF
GO 

SET IDENTITY_INSERT [dbo].[Core_User] ON 
INSERT [dbo].[Core_User] ([Id], [UserGuid], [FullName], [IsDeleted], [CreatedOn], [UpdatedOn], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (2, '4653d7e5-f5ca-4813-a782-3385ca9739bc', N'System', 1, CAST(N'2016-05-12 23:44:13.297' AS DateTime), CAST(N'2016-05-12 23:44:13.297' AS DateTime), N'system@SimplCommerce.com', N'SYSTEM@SIMPLCOMMERCE.COM', N'system@SimplCommerce.com', N'SYSTEM@SIMPLCOMMERCE.COM', 0, N'AQAAAAEAACcQAAAAEAEqSCV8Bpg69irmeg8N86U503jGEAYf75fBuzvL00/mr/FGEsiUqfR0rWBbBUwqtw==', N'9e87ce89-64c0-45b9-8b52-6e0eaa79e5b8', N'8620916f-e6b6-4f12-9041-83737154b339', NULL, 0, 0, CAST(N'2050-05-12 23:44:13.297' AS DateTime), 1, 0)
INSERT [dbo].[Core_User] ([Id], [UserGuid], [FullName], [IsDeleted], [CreatedOn], [UpdatedOn], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (10, '1FFF10CE-0231-43A2-8B7D-C8DB18504F65', N'Shop Admin', 0, CAST(N'2016-05-12 23:44:13.297' AS DateTime), CAST(N'2016-05-12 23:44:13.297' AS DateTime), N'admin@SimplCommerce.com', N'ADMIN@SIMPLCOMMERCE.COM', N'admin@SimplCommerce.com', N'ADMIN@SIMPLCOMMERCE.COM', 0, N'AQAAAAEAACcQAAAAEAEqSCV8Bpg69irmeg8N86U503jGEAYf75fBuzvL00/mr/FGEsiUqfR0rWBbBUwqtw==', N'9e87ce89-64c0-45b9-8b52-6e0eaa79e5b7', N'8620916f-e6b6-4f12-9041-83737154b338', NULL, 0, 0, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Core_User] OFF
GO

INSERT [dbo].[Core_UserRole] ([UserId], [RoleId]) VALUES (10, 1)
GO

SET IDENTITY_INSERT [dbo].[Core_EntityType] ON 
INSERT [dbo].[Core_EntityType] ([Id], [Name], [RoutingController], [RoutingAction], [IsMenuable]) VALUES (1, N'Category', N'Category', N'CategoryDetail', 1)
INSERT [dbo].[Core_EntityType] ([Id], [Name], [RoutingController], [RoutingAction], [IsMenuable]) VALUES (2, N'Brand', N'Brand', N'BrandDetail', 1)
INSERT [dbo].[Core_EntityType] ([Id], [Name], [RoutingController], [RoutingAction], [IsMenuable]) VALUES (3, N'Product', N'Product', N'ProductDetail', 0)
INSERT [dbo].[Core_EntityType] ([Id], [Name], [RoutingController], [RoutingAction], [IsMenuable]) VALUES (4, N'Page', N'Page', N'PageDetail', 1)
INSERT [dbo].[Core_EntityType] ([Id], [Name], [RoutingController], [RoutingAction], [IsMenuable]) VALUES (5, N'Vendor', N'Vendor', N'VendorDetail', 0)
INSERT [dbo].[Core_EntityType] ([Id], [Name], [RoutingController], [RoutingAction], [IsMenuable]) VALUES (6, N'NewsCategory', N'NewsCategory', N'NewsCategoryDetail', 1)
INSERT [dbo].[Core_EntityType] ([Id], [Name], [RoutingController], [RoutingAction], [IsMenuable]) VALUES (7, N'NewsItem', N'NewsItem', N'NewsItemDetail', 0)
SET IDENTITY_INSERT [dbo].[Core_EntityType] OFF
GO


SET IDENTITY_INSERT [dbo].[Core_Widget] ON 
INSERT [dbo].[Core_Widget] ([Id], [Code], [CreateUrl], [CreatedOn], [EditUrl], [IsPublished], [Name], [ViewComponentName]) VALUES (1, N'CarouselWidget', N'widget-carousel-create', CAST(N'2016-06-19 00:00:00.0000000' AS DateTime2), N'widget-carousel-edit', 1, N'Carousel Widget', N'CarouselWidget')
INSERT [dbo].[Core_Widget] ([Id], [Code], [CreateUrl], [CreatedOn], [EditUrl], [IsPublished], [Name], [ViewComponentName]) VALUES (2, N'HtmlWidget', N'widget-html-create', CAST(N'2016-06-24 00:00:00.0000000' AS DateTime2), N'widget-html-edit', 1, N'Html Widget', N'HtmlWidget')
INSERT [dbo].[Core_Widget] ([Id], [Code], [CreateUrl], [CreatedOn], [EditUrl], [IsPublished], [Name], [ViewComponentName]) VALUES (3, N'ProductWidget', N'widget-product-create', CAST(N'2016-07-08 00:00:00.0000000' AS DateTime2), N'widget-product-edit', 1, N'Product Widget', N'ProductWidget')
INSERT [dbo].[Core_Widget] ([Id], [Code], [CreateUrl], [CreatedOn], [EditUrl], [IsPublished], [Name], [ViewComponentName]) VALUES (4, N'CategoryWidget', N'widget-category-create', CAST(N'2016-07-08 00:00:00.0000000' AS DateTime2), N'widget-category-edit', 1, N'Category Widget', N'CategoryWidget')
INSERT [dbo].[Core_Widget] ([Id], [Code], [CreateUrl], [CreatedOn], [EditUrl], [IsPublished], [Name], [ViewComponentName]) VALUES (5, N'SpaceBarWidget', N'widget-spacebar-create', CAST(N'2016-07-08 00:00:00.0000000' AS DateTime2), N'widget-spacebar-edit', 1, N'SpaceBar Widget', N'SpaceBarWidget')
INSERT [dbo].[Core_Widget] ([Id], [Code], [CreateUrl], [CreatedOn], [EditUrl], [IsPublished], [Name], [ViewComponentName]) VALUES (6, N'SimpleProductWidget', N'widget-simple-product-create', CAST(N'2016-07-08 00:00:00.0000000' AS DateTime2), N'widget-simple-product-edit', 1, N'Simple Product Widget', N'SimpleProductWidget')
SET IDENTITY_INSERT [dbo].[Core_Widget] OFF
GO

SET IDENTITY_INSERT [dbo].[Core_WidgetZone] ON 
INSERT [dbo].[Core_WidgetZone] ([Id], [Description], [Name]) VALUES (1, NULL, N'Home Featured')
INSERT [dbo].[Core_WidgetZone] ([Id], [Description], [Name]) VALUES (2, NULL, N'Home Main Content')
INSERT [dbo].[Core_WidgetZone] ([Id], [Description], [Name]) VALUES (3, NULL, N'Home After Main Content')
SET IDENTITY_INSERT [dbo].[Core_WidgetZone] OFF

SET IDENTITY_INSERT [dbo].[Cms_Menu] ON
INSERT [dbo].[Cms_Menu] ([Id], [IsPublished], [IsSystem], [Name]) VALUES (1, 1, 1, N'Customer services')
INSERT [dbo].[Cms_Menu] ([Id], [IsPublished], [IsSystem], [Name]) VALUES (2, 1, 1, N'Information')
SET IDENTITY_INSERT [dbo].[Cms_Menu] OFF
GO


select * from [Localization_Resource]

SET IDENTITY_INSERT [dbo].[Localization_Culture] ON
INSERT [dbo].[Localization_Culture] ([Id], [Name]) VALUES (3, N'pt-BR')
SET IDENTITY_INSERT [dbo].[Localization_Culture] OFF
GO

INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Register', N'Cadastro')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Hello {0}!', N'Olá {0}!')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Log in', N'Entrar')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Log off', N'Sair')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'The Email field is required.', N'O campo Email é obrigatório. ')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Email', N'Email')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'User List', N'Lista de usuários')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Remember me?', N'Lembrar?')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Password', N'Senha')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Use a local account to log in.', N'Entre com seu usuário e senha ')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Register as a new user?', N'Cadastrar-se como novo usuário? ')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Forgot your password?', N'Esqueceu a senha?')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Use another service to log in.', N'Logar utilizando outro serviço')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Full name', N'Nome completo')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Confirm password', N'Confirmar senha')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Create a new account.', N'Criar uma conta.')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'All', N'Todos')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Home', N'Início')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Add to cart', N'Adicionar ao carrinho')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Product detail', N'Detalhes do produto')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Product specification', N'Especificações do produto')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Rate this product', N'Avalie este produto')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Review comment', N'Comentário')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Review title', N'Título da avaliação')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Posted by', N'Postado pro')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Submit review', N'Enviar avaliação')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'You have', N'Você tem')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'products in your cart', N'produtos no carrinho')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Continue shopping', N'Continuar comprando')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'View cart', N'Ver carrinho')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'The product has been added to your cart', N'O produto foi adicionado ao carrinho')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Cart subtotal', N'Subtotal')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Shopping Cart', N'Carrinho de compras')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Product', N'Produto')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Price', N'Preço')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Quantity', N'Quantidade')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'There are no items in this cart.', N'O carrinho está vazio.')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Go to shopping', N'a comprar')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Order summary', N'Resumo do pedido')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Subtotal', N'Subtotal')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Process to Checkout', N'Próxima etapa')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Shipping address', N'Endereço de entrega')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Add another address', N'Adicionar outro endereço')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Contact name', N'Nome completo')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Address', N'Endereço')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'State or Province', N'Estado')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'District', N'Cidade')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Phone', N'Telefone')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Order', N'Pedido')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'products', N'produtos')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'reviews', N'avaliações')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Add Review', N'Adicionar avaliação')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Customer reviews', N'Avaliações de quem comprou')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Your review will be showed within the next 24h.', N'Sua avaliação será publicada dentro de 24h.')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Thank you {0} for your review', N'Muito obrigado pela avaliação, {0}')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Rating average', N'Média das avaliações')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'stars', N'estrelas')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Filter by', N'Filtrar por')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Category', N'Categoria')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Brand', N'Marca')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'Sort by:', N'Ordenar por:')
INSERT [dbo].[Localization_Resource] ([CultureId], [Key], [Value]) VALUES (3, N'results', N'resultados')

