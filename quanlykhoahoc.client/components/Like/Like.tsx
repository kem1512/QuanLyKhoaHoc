import { Button, Group } from "@mantine/core";
import {
  IconMessage,
  IconThumbUp,
  IconThumbUpFilled,
} from "@tabler/icons-react";
import { LikeBlogClient, LikeBlogCreate } from "../../app/web-api-client";
import { handleSubmit } from "../../lib/helper";

export default function Like({
  blogId,
  likeCount,
  comemntCount,
  isLike,
  mutate,
}: {
  blogId: number;
  likeCount: number;
  comemntCount: number;
  isLike: boolean;
  mutate: any;
}) {
  const LikeBlogService = new LikeBlogClient();

  return (
    <Group>
      <Button
        variant="transparent"
        size="compact-xs"
        leftSection={isLike ? <IconThumbUpFilled /> : <IconThumbUp />}
        onClick={() =>
          handleSubmit(
            () => {
              var result = isLike
                ? LikeBlogService.deleteEntity(blogId)
                : LikeBlogService.createEntity({ blogId } as LikeBlogCreate);
              return result;
            },
            isLike ? "Xóa Thành Công" : "Thích Thành Công", mutate
          )
        }
      >
        {likeCount}
      </Button>
      <Button
        variant="transparent"
        size="compact-xs"
        leftSection={<IconMessage />}
      >
        {comemntCount}
      </Button>
    </Group>
  );
}
