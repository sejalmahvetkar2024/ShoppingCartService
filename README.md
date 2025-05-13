
# EShoppingZone

## Overview
EShoppingZone is an e-commerce website built using ASP.NET Core. This platform facilitates the buying and selling of products online. It supports two primary roles: merchants, who sell products, and customers, who purchase them.

## Features
- **User Registration and Login**: Users can create an account and log in using GitHub or their registered credentials.
- **Product Browsing**: Users can browse products by category, including electronics, books, apparel, and personal-care items.
- **Shopping Cart**: Users can add or remove products from their cart.
- **Checkout and Payment**: Users can proceed to checkout and make payments via wallet or cash on delivery.
- **Profile Management**: Users can update their profile information.
- **Order History**: Users can view their previous orders and transactions.

## Detailed Requirements
- **Search Products**: Users can search for products without logging in.
- **Login for Purchase**: Users must log in or create an account to place an order.
- **Profile Updates**: Users can update their profile information as needed.
- **Payment Options**: Wallet payment and cash on delivery are available.
- **Order History**: Users can view their previous orders and transactions.

## Class Diagram
- **Profile-service**
- **Product-service**
- **Cart-service**
- **Order-service**
- **Wallet-service**
- **Website-controller**

## Use Case Diagram
The use case diagram illustrates the interactions between users and the system, detailing the various functionalities available.

## Getting Started
### Prerequisites
- .NET Core SDK
- SQL Server

### Installation
1. Clone the repository:
   ```sh
   git clone https://github.com/your-repo/eshoppingzone.git
   ```
2. Navigate to the project directory:
   ```sh
   cd eshoppingzone
   ```
3. Restore the dependencies:
   ```sh
   dotnet restore
   ```

### Running the Application
1. Update the database connection string in `appsettings.json`.
2. Apply migrations and update the database:
   ```sh
   dotnet ef database update
   ```
3. Run the application:
   ```sh
   dotnet run
   ```

### Usage
- Register or log in to your account.
- Browse products by category.
- Add products to your cart.
- Proceed to checkout and choose a payment method.
- View and manage your profile and order history.
