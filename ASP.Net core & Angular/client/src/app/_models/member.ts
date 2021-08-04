import { Photo } from "./photo";

export interface Member {
    id: number;
    userName: string;
    created: Date;
    lastActive: Date;
    dayOfBirth: Date;
    photos: Photo[];
}