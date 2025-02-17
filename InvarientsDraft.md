# Invariants Draft

## Administrator capacity limit

**Description**: Each *store* have limited number of administrator to manage store.

**Reasoning**: Ensuring safe and manageable *store* by limited number of user.

## Order cancellation time limit

**Description** : *Customer* cannot cancele once the order has been confirmed.

**Reasoning** : minimizing loss as order canceletion after confirm can be expenses to *store*

## Order item limit

**Description** : *Customer* can only *order* limited items.

**Reasoning** : This is to prevent for missusing or avoiding cancellation of product at last hour.

## Order active

**Description** : Only active *product* can be listed in *store*.

**Reasoning** : **Administrator** can manage the product.

## Product can have many category
**Description** : *Product* have list of *category* and vise versa.

**Reasoning** : one can be in different *category* like it can be household and electronic.

## Category can have different Invarient
**Description** : *Category* can have different properties so it has different *invarient*.

**Reasoning** : List of *invarient* can help to list different properties or attribute of category.

## Invarient can have different InvarientOption

**Description** : *Invarient* can have different *InvarientOption* so it be distinct *Invarient* like color can be *invarient* while red, blue, green etc can be *invarientOption* .

**Reasoning** : List of *invarientOption* can help to distinct different *Invarient* attribute.

## Product can have many ProductItem 

**Description** : *Product* can have different *ProductItem* so it be distinct *product*.

**Reasoning** : List of *ProductItem* can help to distinct different *product* according to their attribute.

## Customer can add different shipping address 
**Description** : *Customer* can have different shipping *address* like work, home etc.

**Reasoning** : List of *address* can be helpfull to make deliver the order according to customer preference.