import { Post } from "./post";
import { User } from "./user";

export interface UserLike {
    id: number;
    user: User;
    userId: string;
    post: Post;
    postId: string;
}