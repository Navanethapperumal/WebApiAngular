export class MovieInformation {
  movieId: string;
  name: string;
  releaseYear: Date;
  plot: string;
  poster: string;
  actors: MovieActor[];
  producer: MovieProducer;
}

export class Person {
  name: string;
  gender: string;
  dob: Date;
  bio: string;
}
export class MovieActor extends Person {
  actorId: string;
}

export class MovieProducer extends Person {
  producerId: string;
}
