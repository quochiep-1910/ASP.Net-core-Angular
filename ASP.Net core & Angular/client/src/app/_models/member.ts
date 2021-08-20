import { Photo } from "./photo";

export interface Member {
    id: number;
    username: string;
    created: Date;
    photoUrl: string;
    age: number;
    lastActive: Date;
    dayOfBirth: Date;
    introduction: string;
    lookingFor: string;
    interests: string;
    city: string;
    country: string;
    photos: Photo[];
}