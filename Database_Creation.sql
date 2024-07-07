CREATE TABLE admins (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(191) NOT NULL,
    profile VARCHAR(191) NULL,
    email VARCHAR(191) NOT NULL,
    number VARCHAR(191) NOT NULL,
    role VARCHAR(191) NOT NULL,
    password VARCHAR(191) NOT NULL,
    remember_token VARCHAR(100) NULL,
    created_at DATETIME NULL,
    updated_at DATETIME NULL
);

CREATE TABLE banners (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    banner_category VARCHAR(191) NOT NULL,
    banner_title VARCHAR(191) NOT NULL,
    banner_description VARCHAR(191) NOT NULL,
    banner_image VARCHAR(191) NULL,
    link VARCHAR(191) NULL,
    created_at DATETIME NULL,
    updated_at DATETIME NULL
);

CREATE TABLE campaigns (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    campaign_for VARCHAR(191) NOT NULL,
    campaign_name VARCHAR(191) NOT NULL,
    campaign_image VARCHAR(191) NOT NULL,
    image_type VARCHAR(20) NOT NULL CHECK (image_type IN ('horizontal', 'vertical')),
    percentage INT NOT NULL,
    start DATETIME NOT NULL DEFAULT '2024-01-31 12:49:58',
    [end] DATETIME NOT NULL DEFAULT '2024-01-31 12:49:58',
    created_at DATETIME NULL,
    updated_at DATETIME NULL
);

CREATE TABLE campaign_products (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    campaign_id INT NOT NULL,
    product_id INT NOT NULL,
    created_at DATETIME NULL,
    updated_at DATETIME NULL
);

CREATE TABLE configs (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(191) NOT NULL,
    email VARCHAR(191) NOT NULL,
    number VARCHAR(191) NOT NULL,
    address VARCHAR(191) NOT NULL,
    logo VARCHAR(191) NOT NULL,
    created_at DATETIME NULL,
    updated_at DATETIME NULL
);

CREATE TABLE failed_jobs (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    uuid VARCHAR(191) NOT NULL,
    connection TEXT NOT NULL,
    queue TEXT NOT NULL,
    payload TEXT NOT NULL,
    exception TEXT NOT NULL,
    failed_at DATETIME NOT NULL DEFAULT GETDATE()
);

CREATE TABLE migrations (
    id INT PRIMARY KEY IDENTITY(1,1),
    migration VARCHAR(191) NOT NULL,
    batch INT NOT NULL
);

CREATE TABLE orders (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    order_id VARCHAR(191) NOT NULL,
    name VARCHAR(191) NOT NULL,
    number VARCHAR(191) NOT NULL,
    email VARCHAR(191) NULL,
    address VARCHAR(191) NOT NULL,
    shipping_id BIGINT NOT NULL,
    price DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    message TEXT NULL,
    status VARCHAR(20) NOT NULL CHECK (status IN ('pending', 'processing', 'shipping', 'return', 'cancel', 'damage', 'delieverd')),
    created_at DATETIME NULL,
    updated_at DATETIME NULL
);

CREATE TABLE order_products (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    order_id BIGINT NOT NULL,
    product_id BIGINT NOT NULL,
    price DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    qnt BIGINT NOT NULL,
    created_at DATETIME NULL,
    updated_at DATETIME NULL
);

CREATE TABLE password_resets (
    email VARCHAR(191) NOT NULL,
    token VARCHAR(191) NOT NULL,
    created_at DATETIME NULL
);

CREATE TABLE password_reset_tokens (
    email VARCHAR(191) NOT NULL,
    token VARCHAR(191) NOT NULL,
    created_at DATETIME NULL
);

CREATE TABLE personal_access_tokens (
    id BIGINT PRIMARY KEY IDENTITY(1,1),
    tokenable_type VARCHAR(191) NOT NULL,
    tokenable_id BIGINT NOT NULL,
    name VARCHAR(191) NOT NULL,
    token VARCHAR(64) NOT NULL,
    abilities TEXT NULL,
    last_used_at DATETIME NULL,
    expires_at DATETIME NULL,
    created_at DATETIME NULL,
    updated_at DATETIME NULL
);

CREATE TABLE products (
  id bigint PRIMARY KEY IDENTITY(1,1),
  category_id int NOT NULL,
  name varchar(191) NOT NULL,
  slugs varchar(191) NOT NULL,
  short_description nvarchar(max) NOT NULL,
  description nvarchar(max) NOT NULL,
  discount bigint NOT NULL DEFAULT 0,
  price decimal(10,2) NOT NULL DEFAULT 0.00,
  link varchar(191) NULL,
  stock_status int NOT NULL DEFAULT 1,
  featured int NOT NULL DEFAULT 0,
  popular int NOT NULL DEFAULT 0,
  status int NOT NULL DEFAULT 1,
  seo_title nvarchar(191) NULL,
  seo_description nvarchar(max) NULL,
  seo_tags varchar(191) NULL,
  created_at datetime NULL,
  updated_at datetime NULL
);

CREATE TABLE product_categories (
  id bigint PRIMARY KEY IDENTITY(1,1),
  parent_category_id int NULL,
  category_name varchar(191) NOT NULL,
  slugs varchar(191) NOT NULL,
  category_image varchar(191) NULL,
  category_icon varchar(191) NULL,
  seo_title nvarchar(191) NULL,
  seo_description nvarchar(max) NULL,
  seo_tags varchar(191) NULL,
  created_at datetime NULL,
  updated_at datetime NULL
);

CREATE TABLE product_photos (
  id bigint PRIMARY KEY IDENTITY(1,1),
  product_id int NOT NULL,
  image varchar(191) NOT NULL,
  created_at datetime NULL,
  updated_at datetime NULL
);

CREATE TABLE product_services (
  id bigint PRIMARY KEY IDENTITY(1,1),
  product_id int NOT NULL,
  service_id int NOT NULL,
  created_at datetime NULL,
  updated_at datetime NULL
);

CREATE TABLE services (
  id bigint PRIMARY KEY IDENTITY(1,1),
  logo varchar(191) NULL,
  message nvarchar(max) NOT NULL,
  created_at datetime NULL,
  updated_at datetime NULL
);

CREATE TABLE shippings (
  id bigint PRIMARY KEY IDENTITY(1,1),
  name varchar(191) NOT NULL,
  price decimal(10,2) NOT NULL DEFAULT 0.00,
  created_at datetime NULL,
  updated_at datetime NULL
);

CREATE TABLE users (
  id bigint PRIMARY KEY IDENTITY(1,1),
  name varchar(191) NOT NULL,
  email varchar(191) NOT NULL,
  number varchar(191) NOT NULL,
  email_verified_at datetime NULL,
  password varchar(191) NOT NULL,
  remember_token varchar(100) NULL,
  created_at datetime NULL,
  updated_at datetime NULL
);

INSERT INTO admins (name, profile, email, number, role, password, remember_token, created_at, updated_at) 
VALUES
	('Esmail Khalifa', NULL, 'esmailkhalifa010@gmail.com', '923', 'superAdmin', '$2y$12$BPaj3dy3UWZbKPdq5BDWtO3NjxTogVl.IPJb/dhtNwzDqP2Oq5ema', '2024-02-15 04:10:10', '2024-02-15 04:10:10', '2024-02-15 04:10:10'),
	('Noman', NULL, 'lunareqifa@mailinator.com', '396', 'superAdmin', '$2y$12$st1wHQ.acJyGqqpb8Ad.k.guIxpcCRiNftl3BEFrJtKJkp0YGbM1O', '2024-02-15 04:10:10', '2024-02-15 04:10:10', '2024-02-07 04:41:30');

INSERT INTO banners (banner_category, banner_title, banner_description, banner_image, link, created_at, updated_at)
VALUES
	('7', 'Updated Banner old', 'Commodo libero omnis aaaa', 'BAN1834150-150224.jpg', NULL, '2024-01-31 13:13:40', '2024-02-15 00:30:22');

INSERT INTO campaigns (campaign_for, campaign_name, campaign_image, image_type, percentage, created_at, updated_at)
VALUES
	('Enim mollit et dolor', 'Bert Roth', 'CAMP134383-150224.jpg', 'horizontal', 75, '2024-02-15 03:44:44', '2024-02-15 04:09:50'),
	('Accusantium temporib', 'Neve Welch', 'CAMP64757-150224.jpg', 'vertical', 64, '2024-02-15 04:10:10', '2024-02-15 04:10:10');

-- Inserting into campaign_products
INSERT INTO campaign_products (campaign_id, product_id, created_at, updated_at)
VALUES
    (1, 1, '2024-02-09 13:45:32', '2024-02-09 13:45:32'),
    (2, 1, '2024-02-09 15:02:12', '2024-02-09 15:02:12'),
    (2, 2, '2024-02-09 15:02:12', '2024-02-09 15:02:12'),
    (2, 3, '2024-02-09 15:02:12', '2024-02-09 15:02:12'),
    (2, 4, '2024-02-09 15:03:52', '2024-02-09 15:03:52');

-- Inserting into configs
INSERT INTO configs (name, email, number, address, logo, created_at, updated_at)
VALUES
    ('Familly Bazar', 'esmailkhalifa010@gmail.com', '01753241202', '71/A Zigatola Dhanmondi', 'CONFIG1206249-080224.png', '2024-02-08 13:01:57', '2024-02-08 13:01:57');

------------ NULL Problem --------------
-- Inserting into migrations
INSERT INTO migrations (migration, batch)
VALUES
    ('2014_10_12_000000_create_users_table', 1),
    ('2014_10_12_100000_create_password_reset_tokens_table', 1),
    ('2014_10_12_100000_create_password_resets_table', 1),
    ('2019_08_19_000000_create_failed_jobs_table', 1),
    ('2019_12_14_000001_create_personal_access_tokens_table', 1);

-- Inserting into orders
INSERT INTO orders (order_id, name, number, email, address, shipping_id, price, message, status, created_at, updated_at)
VALUES
    ('ORD20240207162043KC0C', 'fufyuf', '765755776', NULL, 'htrstrstsrstr', 3, 709.80, '', 'pending', '2024-02-07 10:20:43', '2024-02-07 10:20:43'),
    ('ORD20240207162510HVMO', 'noman', '079785764747456', NULL, 'jajira', 4, 4697.86, '', 'pending', '2024-02-07 10:25:10', '2024-02-07 10:25:10'),
    ('ORD20240207211900ICVK', 'Perferendis ut recus', '8', NULL, 'Eius mollit excepteu', 3, 4637.86, NULL, 'processing', '2024-02-07 15:19:00', '2024-02-08 13:43:54'),
    ('ORD20240207212033JQ0V', 'Fugit esse cupidita', '89', NULL, 'Qui odit quisquam la', 4, 359.04, NULL, 'processing', '2024-02-07 15:20:33', '2024-02-08 13:42:58'),
    ('ORD20240207212422NILM', 'Jinia', '01780002040', NULL, '71/A Zigatola Dhanmondi, Dhaka ', 3, 117.75, NULL, 'return', '2024-02-07 15:24:22', '2024-02-08 02:39:16');

-- Inserting into order_products
INSERT INTO order_products (order_id, product_id, price, qnt, created_at, updated_at)
VALUES
    (5, 7, 649.80, 1, '2024-02-07 10:20:43', '2024-02-07 10:20:43'),
    (6, 7, 649.80, 4, '2024-02-07 10:25:10', '2024-02-07 10:25:10'),
    (6, 8, 409.20, 3, '2024-02-07 10:25:10', '2024-02-07 10:25:10'),
    (6, 4, 375.53, 2, '2024-02-07 10:25:10', '2024-02-07 10:25:10'),
    (7, 7, 649.80, 4, '2024-02-07 15:19:00', '2024-02-07 15:19:00');

-- Inserting into products
INSERT INTO products (category_id, name, slugs, short_description, description, discount, price, link, stock_status, featured, popular, status, seo_title, seo_description, seo_tags, created_at, updated_at)
VALUES
    (2, 'Alexandra Stafford', 'alexandra-stafford', 'Aute qui voluptatem', 'Provident, eveniet, .hgfg', 90, 375.00, 'Suscipit suscipit ut', 1, 1, 0, 1, 'Elit enim iure vel', 'Explicabo Alias pro', 'Quis voluptate sequi', '2024-01-31 13:15:29', '2024-01-31 13:15:29'),
    (1, 'Yoko Horn', 'yoko-horn', 'Sint sit delectus i', 'Consequatur? Error s.hfjjh', 53, 170.00, 'Voluptatem nostrum', 1, 0, 1, 1, 'Eiusmod proident no', 'Mollit occaecat alia', 'Ipsam incidunt pers', '2024-01-31 13:15:47', '2024-01-31 13:15:47'),
    (1, 'Shay Jones', 'shay-jones', 'In provident incidu', 'Do ullamco nobis des.uguyg', 20, 219.00, 'Laboriosam qui non', 1, 0, 0, 1, 'Ut harum animi nost', 'Harum ab ipsum illo', 'Dolor autem cillum u', '2024-01-31 13:16:25', '2024-01-31 13:16:25');

-- Inserting into product_categories
INSERT INTO product_categories (parent_category_id, category_name, slugs, category_image, category_icon, seo_title, seo_description, seo_tags, created_at, updated_at)
VALUES
    (NULL, 'Gadgets', 'gadgets', 'Lane Petersen1424374-150224.png', '2024-01-31 13:15:29', 'Rerum veniam vel ut', 'Molestiae architecto', 'Non animi officia d', '2024-02-14 23:49:03', '2024-02-14 23:57:53'),
    (NULL, 'Maggie Curry', 'maggie-curry', 'CAT72496-150224.jpg', '2024-01-31 13:15:29', 'Corrupti quis facil', 'Blanditiis ipsum el', 'Exercitation ex aute', '2024-02-15 04:18:17', '2024-02-15 05:12:49');

-- Inserting into product_photos
INSERT INTO product_photos (product_id, image, created_at, updated_at)
VALUES
    (1, '1PRO676375-310124.jpg', '2024-01-31 13:15:29', '2024-01-31 13:15:29'),
    (1, '1PRO1003368-310124.jpg', '2024-01-31 13:15:29', '2024-01-31 13:15:29'),
    (2, '2PRO146473-310124.jpg', '2024-01-31 13:15:29', '2024-01-31 13:15:29'),
    (2, '2PRO1441475-310124.jpg', '2024-01-31 13:15:29', '2024-01-31 13:15:29'),
    (3, '3PRO80398-310124.jpg', '2024-01-31 13:15:29', '2024-01-31 13:15:29');

-- Inserting into product_services
INSERT INTO product_services (product_id, service_id, created_at, updated_at) 
VALUES
    (1, 1, '2024-01-31 13:15:29', '2024-01-31 13:15:29'),
    (1, 2, '2024-01-31 13:15:29', '2024-01-31 13:15:29'),
    (1, 3, '2024-01-31 13:15:29', '2024-01-31 13:15:29'),
    (1, 4, '2024-01-31 13:15:29', '2024-01-31 13:15:29'),
    (2, 1, '2024-01-31 13:15:47', '2024-01-31 13:15:29');

-- Inserting into services
INSERT INTO services (logo, message, created_at, updated_at) 
VALUES 
    (NULL, 'Ullamco ut autem qui', '2024-02-15 07:26:32', '2024-02-15 07:26:32');

-- Inserting into shippings
INSERT INTO shippings (name, price, created_at, updated_at)
VALUES 
    ('Out side Dhaka  120.0', 120.0, '2024-02-15 07:28:06', '2024-02-15 07:28:06'),
    ('out side Dhaka  320.0', 320.0, '2024-02-15 07:29:30', '2024-02-15 07:29:30'),
    ('in side Dhaka  40.0', 40.0, '2024-02-15 07:30:56', '2024-02-15 07:30:56'),
    ('in side Dhaka  60.0', 60.0, '2024-02-15 07:32:37', '2024-02-15 07:32:37'),
    ('IN inside Dhaka  80.0', 80.0, '2024-02-15 07:33:57', '2024-02-15 07:33:57'),
    ('I want to take and receive time 3 days. in city', 120.0, '2024-02-15 07:34:30', '2024-02-15 07:34:30'),
    ('I want to have to take 30 days. 3 city / only in days', 1.0, '2024-02-15 07:35:02', '2024-02-15 07:35:02'),
    ('Out side Dhaka  120.0', 120.0, '2024-02-15 07:35:47', '2024-02-15 07:35:47');

-- Inserting into users
INSERT INTO users (name, email, number, email_verified_at, password, remember_token, created_at, updated_at)
VALUES 
    ('Test', 'kkkkkkk','017234234', NULL, 'akil de de ciyasd saek', '1', '2024-02-15 07:40:37', '2024-02-15 07:40:37');
    
