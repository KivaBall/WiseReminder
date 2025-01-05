# API Methods Documentation 📚

## Overview

This document provides detailed information about the available API methods for the **WiseReminder** project. Each method includes its description, HTTP method, and purpose. Refer to the `openapi.json` file in the `docs/` folder for a complete machine-readable specification

---

## Endpoint

### **Users Endpoints**

#### **1. Register User**

- **URL:** `/api/users/register`
- **Method:** `POST`
- **Purpose:** Registers new user

#### **2. Login**

- **URL:** `/api/users/login`
- **Method:** `POST`
- **Purpose:** Logins user

#### **3. Admin Login**

- **URL:** `/api/users/admin-login`
- **Method:** `POST`
- **Purpose:** Logins admin

#### **4. Change Username (FOR USER)**

- **URL:** `/api/users/own/username`
- **Method:** `PUT`
- **Purpose:** Changes user's username

#### **5. Change Password (FOR USER)**

- **URL:** `/api/users/own/password`
- **Method:** `PUT`
- **Purpose:** Changes user's password

#### **6. Delete User (FOR ADMIN)**

- **URL:** `/api/users/{id}`
- **Method:** `DELETE`
- **Purpose:** Deletes user by ID

#### **7. Delete Own User (FOR USER)**

- **URL:** `/api/users/{id}`
- **Method:** `DELETE`
- **Purpose:** Deletes user's user

#### **8. Get User Details by ID (FOR ADMIN)**

- **URL:** `/api/users/{id}`
- **Method:** `GET`
- **Purpose:** Retrieves user details by ID

#### **9. Get Own User Details (FOR USER)**

- **URL:** `/api/users/{id}`
- **Method:** `GET`
- **Purpose:** Retrieves user's user details

---

### **Authors Endpoints**

#### **1. Create Author (FOR ADMIN)**

- **URL:** `/api/authors`
- **Method:** `POST`
- **Purpose:** Creates new author

#### **2. Create Own Author (FOR USER)**

- **URL:** `/api/authors/own`
- **Method:** `POST`
- **Purpose:** Creates user's author

#### **3. Update Author (FOR ADMIN)**

- **URL:** `/api/authors/{id}`
- **Method:** `PUT`
- **Purpose:** Updates author by ID

#### **4. Update Own Author (FOR USER)**

- **URL:** `/api/authors/own`
- **Method:** `PUT`
- **Purpose:** Updates user's author

#### **5. Delete Author (FOR ADMIN)**

- **URL:** `/api/authors/{id}`
- **Method:** `DELETE`
- **Purpose:** Deletes author by ID

#### **6. Delete Own Author (FOR USER)**

- **URL:** `/api/authors/own`
- **Method:** `DELETE`
- **Purpose:** Deletes user's author

#### **7. Get Own Author (FOR USER)**

- **URL:** `/api/authors/own`
- **Method:** `GET`
- **Purpose:** Retrieves user's author

#### **8. Get Author**

- **URL:** `/api/authors/{id}`
- **Method:** `GET`
- **Purpose:** Retrieves author by ID

#### **9. Get All Authors**

- **URL:** `/api/authors`
- **Method:** `GET`
- **Purpose:** Retrieves all authors

#### **10. Get Authors by User**

- **URL:** `/api/authors/user/{userId}`
- **Method:** `GET`
- **Purpose:** Retrieves author by user ID

---

### **Categories Endpoints**

#### **1. Create Category (FOR ADMIN)**

- **URL:** `/api/categories`
- **Method:** `POST`
- **Purpose:** Creates new category

#### **2. Update Category (FOR ADMIN)**

- **URL:** `/api/categories/{id}`
- **Method:** `PUT`
- **Purpose:** Updates category by ID

#### **3. Delete Category (FOR ADMIN)**

- **URL:** `/api/categories/{id}`
- **Method:** `DELETE`
- **Purpose:** Deletes category by ID

#### **4. Get Category by ID**

- **URL:** `/api/categories/{id}`
- **Method:** `GET`
- **Purpose:** Retrieves category by ID

#### **5. Get All Categories**

- **URL:** `/api/categories`
- **Method:** `GET`
- **Purpose:** Retrieves all categories

---

### **Quotes Endpoints**

#### **1. Create Quote (FOR ADMIN)**

- **URL:** `/api/quotes`
- **Method:** `POST`
- **Purpose:** Creates new quote

#### **2. Create Own Quote (FOR USER)**

- **URL:** `/api/quotes/own`
- **Method:** `POST`
- **Purpose:** Creates user's quote

#### **3. Update Quote (FOR ADMIN)**

- **URL:** `/api/quotes/{id}`
- **Method:** `PUT`
- **Purpose:** Updates quote by ID

#### **4. Update Own Quote (FOR USER)**

- **URL:** `/api/quotes/own/{id}`
- **Method:** `PUT`
- **Purpose:** Updates user's quote

#### **5. Delete Quote (FOR ADMIN)**

- **URL:** `/api/quotes/{id}`
- **Method:** `DELETE`
- **Purpose:** Deletes quote by ID

#### **6. Delete Own Quote (FOR USER)**

- **URL:** `/api/quotes/own/{id}`
- **Method:** `DELETE`
- **Purpose:** Deletes user's quote by ID

#### **7. Get Quote by ID**

- **URL:** `/api/quotes/{id}`
- **Method:** `GET`
- **Purpose:** Retrieves quote by ID

#### **8. Get Own Quotes (FOR USER)**

- **URL:** `/api/quotes/own`
- **Method:** `GET`
- **Purpose:** Retrieves user's quotes

#### **9. Get Quotes by Author**

- **URL:** `/api/quotes/author/{authorId}`
- **Method:** `GET`
- **Purpose:** Retrieves quotes by author ID

#### **10. Get Quotes by Category**

- **URL:** `/api/quotes/category/{categoryId}`
- **Method:** `GET`
- **Purpose:** Retrieves quotes by category ID

#### **11. Get Random Quote**

- **URL:** `/api/quotes/random`
- **Method:** `GET`
- **Purpose:** Retrieves random quote

#### **12. Get Multiple Random Quotes**

- **URL:** `/api/quotes/random/{amount}`
- **Method:** `GET`
- **Purpose:** Retrieves specified number of random quotes

#### **13. Get Daily Quote**

- **URL:** `/api/quotes/daily`
- **Method:** `GET`
- **Purpose:** Retrieves daily quote

#### **14. Get Recent Quotes**

- **URL:** `/api/quotes/recent`
- **Method:** `GET`
- **Purpose:** Retrieves the most recent quote

#### **15. Get Multiple Recent Quotes**

- **URL:** `/api/quotes/recent/{amount}`
- **Method:** `GET`
- **Purpose:** Retrieves specified number of the most recent quotes