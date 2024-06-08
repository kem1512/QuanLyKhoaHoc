"use client";

import { Button, Grid, SimpleGrid, TextInput } from "@mantine/core";
import DashboardLayout from "../../../../components/Layout/DashboardLayout";
import { BlogClient, BlogCreate, IBlogCreate } from "../../../web-api-client";
import { handleSubmit } from "../../../../lib/helper";
import { useState } from "react";
import { DateTimePicker } from "@mantine/dates";
import Editor from "../../../../components/Editor/Editor";

export default function DashboardBlogCreate() {
  const BlogService = new BlogClient();

  const [blog, setBlog] = useState<IBlogCreate>({
    content: "",
    createTime: new Date(),
    numberOfComments: 0,
    numberOfLikes: 0,
    title: "",
  });

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
          <Button
            size="xs"
            onClick={() =>
              handleSubmit(() => {
                return BlogService.createBlog(blog as BlogCreate);
              }, "Thêm Thành Công")
            }
          >
            Xác Nhận
          </Button>
        </Grid.Col>
      </Grid>
    </DashboardLayout>
  );
}
