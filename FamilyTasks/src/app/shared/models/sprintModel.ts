export class SprintModel{
    public firstName: string;
    public goal: string;
    public name: string;
    public id: string;
    public start_Date: Date;
    public end_Date: Date;

    constructor(init: Partial<SprintModel>) {
        Object.assign(this, init);
    }
}