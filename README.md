# Real-Time Messaging Application

This project is a **real-time messaging application** built with **ASP.NET Core** and **SignalR** to allow users to send messages instantly, upload attachments, and track online status. The application provides a chat system with authentication and real-time notifications.

## Features

- **Real-Time Messaging**: Messages are delivered instantly to recipients using SignalR.
- **User Authentication**: JWT-based authentication and user management with ASP.NET Identity.
- **File Attachments**: Users can send images or other file types as part of their messages.
- **Online Status**: Tracks and displays the online/offline status of users in real-time.
- **Message Read Notification**: Notifies when a message is read by the recipient.

## Technologies Used

- **ASP.NET Core**: A powerful framework for building web applications.
- **SignalR**: Real-time communication between the server and the client.
- **Entity Framework Core**: ORM for data access.
- **JWT Authentication**: Secure and stateless authentication for API requests.
- **SQL Server**: For database management.

## API Endpoints

- `POST /api/auth/login`: Login an existing user.
- `POST /api/auth/register`: Register a new user.
- `POST /api/messages/send`: Send a message to another user.
- `GET /api/users`: Fetch all users and their online statuses.

## Real-Time Features

The project utilizes **SignalR** to enable real-time interactions:

- **User Online/Offline Notifications**: When a user connects or disconnects, the other users are notified about their status.
- **Message Delivery**: Sent messages are delivered in real-time to the recipient's client.
- **Message Read Tracking**: Once a recipient reads the message, the sender is notified.


