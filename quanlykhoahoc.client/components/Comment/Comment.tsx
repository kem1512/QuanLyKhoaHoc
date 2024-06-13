"use client";

import {
  Text,
  Avatar,
  Group,
  TypographyStylesProvider,
  Paper,
  Stack,
  Menu,
  ActionIcon,
  Alert,
  Button,
} from "@mantine/core";
import classes from "./Comment.module.css";
import useSWR, { mutate } from "swr";
import {
  CommentBlogClient,
  CommentBlogCreate,
  CommentBlogUpdate,
  ICommentBlogMapping,
} from "../../app/web-api-client";
import { handleSubmit, useQuery } from "../../lib/helper";
import Editor from "../Editor/RichTextEditor/RichTextEditor";
import { useEffect, useState } from "react";
import ActionButton from "../Helper/ActionButton";
import { IconDots } from "@tabler/icons-react";
import Loading from "../Loading/Loading";
import { modals } from "@mantine/modals";
import { useSelector } from "react-redux";

export function Comment({ blogId }: { blogId: number }) {
  const query = useQuery();

  const CommentBlogService = new CommentBlogClient();

  const user = useSelector((state: any) => state.auth.user);

  const [commentBlog, setCommentBlog] = useState<ICommentBlogMapping>({
    blogId,
    content: "",
    parentId: null,
  });

  const { data, mutate, isLoading } = useSWR(
    `/api/commentBlog/${blogId}`,
    () =>
      new CommentBlogClient().getEntities(
        blogId,
        query.filters,
        query.sorts,
        query.page ? parseInt(query.page) : 1,
        query.pageSize ? parseInt(query.pageSize) : 10
      ),
    {
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const handleDelete = (id: number | undefined) => {
    modals.openConfirmModal({
      title: "Xóa Bình Luận",
      children: (
        <Text size="sm">
          Bạn Chắc Chắn Muốn Xóa? Thao Tác Này Sẽ Không Thể Phục Hồi
        </Text>
      ),
      confirmProps: { color: "red" },
      labels: { confirm: "Chắc Chắn", cancel: "Hủy" },
      onConfirm: () =>
        handleSubmit(() => {
          return CommentBlogService.deleteEntity(id);
        }, "Xóa Thành Công").then(() => mutate()),
    });
  };

  return (
    <Stack gap={"sm"}>
      <Editor
        content={commentBlog.content}
        onChange={(e) => setCommentBlog((prev) => ({ ...prev, content: e }))}
      />
      <Group gap={"xs"}>
        <ActionButton
          size="xs"
          action={() =>
            handleSubmit(() => {
              return (
                commentBlog.id
                  ? CommentBlogService.updateEntity(
                      commentBlog.id,
                      commentBlog as CommentBlogUpdate
                    )
                  : CommentBlogService.createEntity(
                      commentBlog as CommentBlogCreate
                    )
              ).then(() => mutate());
            }, `${commentBlog.id ? "Sửa" : "Thêm"} Thành Công`)
          }
        >
          Xác Nhận
        </ActionButton>
        {commentBlog.id || commentBlog.parentId ? (
          <Button
            color="red"
            size="xs"
            onClick={() =>
              setCommentBlog({
                blogId,
                content: "",
                parentId: null,
              })
            }
          >
            Hủy
          </Button>
        ) : null}
      </Group>
      {isLoading ? (
        <Loading />
      ) : data?.items?.length && data.items.length >= 0 ? (
        data.items.map((item) => {
          return (
            <Paper withBorder radius="md" className={classes.comment}>
              <Group justify="space-between">
                <Group>
                  <Avatar
                    src={item.user.avatar}
                    alt={item.user?.username}
                    radius="xl"
                  />
                  <Text fz="sm" fw={"bold"}>
                    {item.user?.username}
                  </Text>
                </Group>
                <Menu>
                  <Menu.Target>
                    <ActionIcon variant="transparent">
                      <IconDots width={20} />
                    </ActionIcon>
                  </Menu.Target>
                  <Menu.Dropdown>
                    {user.email === item.user.email && (
                      <>
                        <Menu.Item
                          onClick={() =>
                            setCommentBlog((prev) => ({ ...prev, id: item.id }))
                          }
                        >
                          Sửa
                        </Menu.Item>
                        <Menu.Item onClick={() => handleDelete(item.id)}>
                          Xóa
                        </Menu.Item>
                      </>
                    )}
                    <Menu.Item
                      onClick={() =>
                        setCommentBlog((prev) => ({
                          ...prev,
                          parentId: item.parentId,
                        }))
                      }
                    >
                      Trả Lời
                    </Menu.Item>
                  </Menu.Dropdown>
                </Menu>
              </Group>
              <TypographyStylesProvider className={classes.body}>
                <div
                  className={classes.content}
                  dangerouslySetInnerHTML={{
                    __html: item?.content,
                  }}
                />
              </TypographyStylesProvider>
            </Paper>
          );
        })
      ) : (
        <Alert>Chưa Có Bình Luận</Alert>
      )}
    </Stack>
  );
}
