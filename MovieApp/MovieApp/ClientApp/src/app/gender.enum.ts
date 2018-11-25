export enum Gender {
  Female,
  Male
}


export namespace Gender {

  export function values() {
    return Object.keys(Gender).filter(
      (type) => isNaN(<any>type) && type !== 'values'
    );
  }
}
