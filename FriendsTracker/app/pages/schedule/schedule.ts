export class Schedule {
    dayId: number;
    from: number;
    to: number;
    eventDescription: string;
    eventId: number;
}

export class ScheduleViewModel {
    public events: Array<Schedule>;
    public userId: String;
}