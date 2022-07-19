import { Post } from "./post";
import { Profile } from "./profile";
import { Subscribe } from "./subscribe";
import { UserLike } from "./userLike";
import { UserSave } from "./userSave";

export interface User {
    id: string;
    name: string;
    userName: string;
    email: string;
    profile: Profile;
    posts: Array<Post>
    savedPosts: Array<UserSave>
    userLikes: Array<UserLike>
    subscribers: Array<Subscribe>
    followers: Array<Subscribe>
}