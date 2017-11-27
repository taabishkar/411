export class Schedule {
    DayId: Number;
    From: Number;
    To: Number;
    EventDescription: String;
    EventId: Number;
}

export class ScheduleViewModel {
    public events: Array<Schedule>;
    public userId: String;
}