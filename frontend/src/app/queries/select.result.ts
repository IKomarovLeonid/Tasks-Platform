export class SelectResult<T>{
  public readonly items: Array<T>;

  public constructor(items: Array<T>, public readonly errorMessage = "") {
    this.items = items;
  }

  public IsSuccess(): boolean{
    return this.errorMessage == "";
  }

}
