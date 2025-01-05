# Subject Area Documentation 📚

## Overview
The **WiseReminder** project revolves around a structured system for managing quotes, authors, categories, and users. This document provides an outline of the core entities, their relationships, and the roles of administrators and users within the system

---

## Core Entities

### 1. **Quotes**
- Quotes are central to the system and represent the main content
- Each quote is:
    - Associated with a specific **author**
    - Categorized under a **category**

### 2. **Authors**
- Authors are individuals to whom quotes are attributed
- **Admin-Controlled Authors**:
    - Created by administrators
    - Not linked to any specific user
    - Used to create system-wide quotes
- **User-Controlled Authors**:
    - Created by registered users
    - Linked to the user who created them
    - Serve as a private or personal collection for the user's quotes

### 3. **Categories**
- Categories group quotes by themes or topics (e.g., "Motivation," "Love," "Wisdom")
- Created and managed exclusively by administrators

### 4. **Users**
- Users register to interact with the system
- Capabilities include:
    - Creating their own authors
    - Adding quotes to their authors
    - Liking or disliking other users' quotes

### 5. **Subscriptions**
- Users can choose from the following subscription tiers:
    - **Free**:
        - Maximum of 5 quotes per user-created author
    - **Iron**:
        - Maximum of 50 quotes per user-created author
    - **Diamond**:
        - Maximum of 1,000 quotes per user-created author

---

## Entity Relationships

- **Quotes**:
    - Linked to an **author**
    - Linked to a **category**
- **Authors**:
    - Can optionally have a **user ID** if created by a user
    - Can exist without a **user ID** if created by an admin
- **Users**:
    - May have their own **author(s)**
    - Interact with other users' quotes (like/dislike)

---

## Roles and Responsibilities

### **Administrator**
- **Capabilities:**
    - Create, manage, and delete authors not linked to any user
    - Create and manage categories
    - Create quotes linked to admin-controlled authors and categories
- **Restrictions:**
    - Cannot modify user-controlled authors
    - Cannot manage quotes tied to user-controlled authors

### **User**
- **Capabilities:**
    - Register and create their own authors
    - Create quotes tied to their personal authors
    - Like or dislike quotes from other users or admin-controlled authors
- **Restrictions:**
    - Cannot exceed the quote limit set by their subscription tier
    - Cannot edit or delete admin-controlled authors or quotes

---

## Subscription System

### **Free Tier**
- Default for all new users
- Users can:
    - Create up to 5 quotes for their personal authors

### **Iron Tier**
- Paid subscription
- Users can:
    - Create up to 50 quotes for their personal authors

### **Diamond Tier**
- Premium subscription
- Users can:
    - Create up to 1,000 quotes for their personal authors