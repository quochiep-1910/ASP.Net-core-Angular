import { Photo } from "./photo";

export interface Member {
    id: number;
    userName: string;
    created: Date;
    lastActive: Date;
    dayOfBirth: Date;
    introduction: string;
    lookingFor: string;
    interests: string;
    city: string;
    country: string;
    photos: Photo[];
}