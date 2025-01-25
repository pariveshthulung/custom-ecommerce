# Ubiquitous Language Draft

## Store
**Defination** : Store is a shop of any kind.
**Usage** : **Administrator** can add list of **product** to their store.
**Usage** : **Store** have limited number of admin account.
**Usage** : **Customer** can only browse from one **store** only at a time.

## Product
**Defination** : **Store** can have different **product**.
**Usage** : **Administrator** can add list of **product** to their **store**.
**Usage** : **Product** can have differnt **productItem** and **productImage**.

## ProductItem
**Defination** : **ProductItem** is distinct product with its own distinctive attribute like price, quantity etc.
**Usage** : **Administrator** can add list of **productItem** to their respective product.

## ProductImage
**Defination** : **ProductImage** is collection of image relation to product.
**Usage** : **Administrator** can add or delete different images to their respective product.

## ProductConfirm
**Defination** : **ProductConfirm** is mapping table for **invarientOption** and  **productItem** which can be distinct final product.
**Usage** : **Administrator** can manage **productCofirm**.

## Customer
**Defination** : Who use the system to browse, search, purchase the **product**.
**Usage** : **Customer** can register on the platform.
**Usage** : **Customer** can make purchase.
**Usage** : **Customer** can manage their **cart**.
**Usage** : **Customer** can make payment.
**Usage** : **Customer** can add their shipping address.

## Order
**Defination** : **Order** made by the **customer**.
**Usage** : **Customer** can place **order**.
**Usage** : **Order** can have list of **products**.

## OrderItem
**Defination** : OrderItem are the products in **Order** which is purchased by **Customer**.
**Usage** : **Customer** can add order list of order items to **Order**.

## Cart
**Defination** : A container to keep record of selected **Product** by item for future purchase.
**Usage** : **Customer** can add product to cart.
**Usage** : Cart can have list of items.
**Usage** : **Customer** can add or remove item from cart.

## CartItem
**Defination** : List of **Product** selected by user for checkout
**Usage** : **Customer** can add **cartItem** to **cart**.
**Usage** : **Customer** can select **cartitem** for checkout..


## Administrator
**Defination** : **Administrator** are the owner of **Store** who manage the **store**.
**Usage** : **Administrator** can add or update **Product**.
**Usage** : **Administrator** manage all the **product** details.

## Category
**Defination** : A **product** can have list of **category** and vise versa.
**Usage** : Only **Adminstrator** can add category to **product**.
**Usage** : **Adminstrator** can add or modify category.

## ProductCategory
**Defination** : It's mapping table for product and category.
**Usage** : **Administrator** can add **ProductCategory**.

## Invarient
**Defination** : **Category** can have list of **invarient** which contain its properties name like shape, size, color etc.
**Usage** : Only **Adminstrator** can manage **invarient**. 
**Usage** : It contain list of **InvarientOption**

## InvarientOption
**Defination** : It contain value of **invarient** like if **invarient** is color then **invarientOption** can be red, green, blue etc.
**Usage** : Only **Adminstrator** can manage **invarientOption**.

## Address
**Defination** : It contain shipping address of **customer**.
**Usage** : Customer can add different shipping address.
