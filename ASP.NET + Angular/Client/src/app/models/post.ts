import { User } from "./user";
import { Comment } from "./comment";
import { UserLike } from "./userLike";
import { UserSave } from "./userSave";

export interface Post {
    id: number;
    imageUrl: string;
    description: string;
    createdBy: string;
    isDeleted: boolean;
    user: User;
    comments: Array<Comment>;
    likes: Array<UserLike>;
    saves: Array<UserSave>;
}