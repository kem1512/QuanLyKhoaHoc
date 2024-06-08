"use client";

import { Grid, TextInput } from "@mantine/core";
import DashboardLayout from "../../../../components/Layout/DashboardLayout";
import { BlogClient, BlogUpdate, IBlogUpdate } from "../../../web-api-client";
import { handleSubmit } from "../../../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import { DateTimePicker } from "@mantine/dates";
import Editor from "../../../../components/Editor/Editor";
import ActionButton from "../../../../components/Helper/ActionButton";

export default function DashboardBlogUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  const BlogService = new BlogClient();

  const { data } = useSWR(`/api/course/${id}`, () => BlogService.getEntity(id), {
    revalidateIfStale: false,
    revalidateOnFocus: false,
    revalidateOnReconnect: false,
  });

  const [blog, setBlog] = useState<IBlogUpdate>({
    content: "",
    createTime: new Date(),
    numberOfComments: 0,
    numberOfLikes: 0,
    title: "",
  });

  useEffect(() => {
    if (data) setBlog(data);
  }, [data]);

  return (
    <DashboardLayout>
      <Grid>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Tiêu Đề"
            placeholder="Nhập Tiêu Đề"
            value={blog.title}
            onChange={(e) =>
              setBlog((prev) => ({ ...prev, title: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Số Bình Luận"
            placeholder="Nhập Số Bình Luận"
            type="number"
            value={blog.numberOfComments}
            onChange={(e) =>
              setBlog((prev) => ({
                ...prev,
                numberOfComments: parseInt(e.target.value),
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>

        <Grid.Col span={{ base: 12, lg: 6 }}>
          <TextInput
            label="Số Lượt Thích"
            placeholder="Nhập Số Lượt Thích"
            type="number"
            value={blog.numberOfLikes}
            onChange={(e) =>
              setBlog((prev) => ({
                ...prev,
                numberOfLikes: parseInt(e.target.value),
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={{ base: 12, lg: 6 }}>
          <DateTimePicker
            withSeconds
            label="Ngày Tạo"
            placeholder="Chọn Ngày Tạo"
            value={blog.createTime}
            onChange={(e) =>
              setBlog((prev) => ({
                ...prev,
                createTime: e,
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
        </Grid.Col>
        <Grid.Col span={12}>
          <Editor
            content={blog.content}
            onChange={(e) => setBlog((prev) => ({ ...prev, content: e }))}
          />
        </Grid.Col>
        <Grid.Col span={12}>
          <ActionButton
            size="xs"
            action={() =>
              handleSubmit(() => {
                return BlogService.updateEntity(blog.id, blog as BlogUpdate);
              }, "Thêm Thành Công")
            }
          >
            Xác Nhận
          </ActionButton>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
