CREATE TABLE [Users] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [full_name] nvarchar(255),
  [fidelity_point] int,
  [email] nvarchar(255),
  [phone] nvarchar(255),
  [address] varchar(2000),
)
GO

CREATE TABLE [Product] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255),
  [product_description] varchar(2000),
  [category_id] int
)
GO

CREATE TABLE [Category] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] nvarchar(255),
  [description] nvarchar(255),
  [rate_id] int,
  [fidelity_point] int
)
GO

CREATE TABLE [OrderDetail] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [order_id] int,
  [date] datetime,
  [user_id] int
)
GO

CREATE TABLE [Order] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [payment_id] int,
  [total_price] int,
  [total_fidelity_point] int
)
GO

CREATE TABLE [Payment] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [date] datetime
)
GO

CREATE TABLE [FromToCalculation] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [rate_id] int,
  [category_id] int,
  [from] int,
  [to] int
)
GO

CREATE TABLE [Rate] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [name] varchar(200),
  [price] int
)
GO

CREATE TABLE [CartDetail] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [user_id] int FOREIGN KEY REFERENCES Users(id),
  [product_id] int,
  [number_of_day] int,
)
GO

CREATE TABLE [ProductOrderDetail] (
  [id] int PRIMARY KEY IDENTITY(1, 1),
  [OrderDetail] int FOREIGN KEY REFERENCES OrderDetail(id),
  [product_id] int FOREIGN KEY REFERENCES Product(id),
  [number_of_day] int,
)
GO

ALTER TABLE [product] ADD FOREIGN KEY ([category_id]) REFERENCES [Category] ([id])
GO

ALTER TABLE [OrderDetail] ADD FOREIGN KEY ([user_id]) REFERENCES [users] ([id])
GO

ALTER TABLE [OrderDetail] ADD FOREIGN KEY ([order_id]) REFERENCES [Order] ([id])
GO

ALTER TABLE [Order] ADD FOREIGN KEY ([payment_id]) REFERENCES [Payment] ([id])
GO

ALTER TABLE [FromToCalculation] ADD FOREIGN KEY ([rate_id]) REFERENCES [Rate] ([id])
GO

ALTER TABLE [FromToCalculation] ADD FOREIGN KEY ([category_id]) REFERENCES [Category] ([id])
GO

ALTER TABLE [CartDetail] ADD FOREIGN KEY ([product_id]) REFERENCES [Product] ([id])
GO
