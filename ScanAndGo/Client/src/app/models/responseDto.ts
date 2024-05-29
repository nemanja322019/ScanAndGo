import { User, UserDto } from "./user";

export interface ResponseDto{
    token:string;
    userDto:User;
    result:string
}