export class Schedule {
    Day: String;
    From: Number;
    To: Number;
    Event: String;
    EventId: Number;
    CountId: number;
}

export class ScheduleViewModel {
    public scheduleArray: Array<Schedule>;
    public userId: String;
}