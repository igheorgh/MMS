export class TaskModel{
    public status: string;
    public description: string;
    public name: string;
    public id: string;
    public user_id: string;
    public sprint_id: string;
    public username: string;

    constructor(init: Partial<TaskModel>) {
        Object.assign(this, init);
    }
}