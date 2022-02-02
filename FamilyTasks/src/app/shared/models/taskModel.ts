export class TaskModel{
    public status: string;
    public description: string;
    public name: string;
    public id: string;

    constructor(init: Partial<TaskModel>) {
        Object.assign(this, init);
    }
}