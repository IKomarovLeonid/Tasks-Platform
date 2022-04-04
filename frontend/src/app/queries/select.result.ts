export class SelectResult<T>{
  public readonly items: Array<T>;

  public constructor(items: Array<T>) {
    this.items = items;
  }

}
