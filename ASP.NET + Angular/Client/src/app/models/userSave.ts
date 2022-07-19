import { Post } from "./post";
import { User } from "./user";

export interface UserSave {
    id: number;
    user: User;
    userId: string;
    post: Post;
    postId: string;
}