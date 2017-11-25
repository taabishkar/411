export class Notification {
    public NotificationId: number;  
    public NotificationTypeId: number;
    public NotificationDetails: string;
    public NotificationAcknowledged: boolean;   
    public CreatedBy: string;
    public CreatedDateTime: Date; 
}
export class FriendRequestResponse{
    public notification: Notification;
    public LoggedInUserId: string
}