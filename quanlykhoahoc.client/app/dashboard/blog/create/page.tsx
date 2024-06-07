"use client";

import { Button, SimpleGrid, TextInput } from "@mantine/core";
import DashboardLayout from "../../../../components/Layout/DashboardLayout";
import { BlogClient, BlogCreate, IBlogCreate } from "../../../web-api-client";
import { handleSubmit } from "../../../../lib/helper";
import { useState } from "react";

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
      <SimpleGrid cols={{ base: 1, lg: 2 }}>
        <TextInput
          label="Tiêu Đề"
          placeholder="Nhập Tiêu Đề"
          value={blog.title}
          onChange={(e) =>
            setBlog((prev) => ({ ...prev, title: e.target.value }))
          }
          labelProps={{ style: { marginBottom: 6 } }}
        />
        <TextInput
          label="Nội Dung"
          placeholder="Nhập Nội Dung"
          value={blog.content}
          onChange={(e) =>
            setBlog((prev) => ({ ...prev, content: e.target.value }))
          }
          labelProps={{ style: { marginBottom: 6 } }}
        />

        <TextInput
          label="Số Bình Luận"
          placeholder="Nhập Số Bình Luận"
          type="number" // Đặt kiểu dữ liệu là number cho ô input
          value={blog.numberOfComments}
          onChange={(e) =>
            setBlog((prev) => ({
              ...prev,
              numberOfComments: parseInt(e.target.value),
            }))
          }
          labelProps={{ style: { marginBottom: 6 } }}
        />

        <TextInput
          label="Số Lượt Thích"
          placeholder="Nhập Số Lượt Thích"
          type="number" // Đặt kiểu dữ liệu là number cho ô input
          value={blog.numberOfLikes}
          onChange={(e) =>
            setBlog((prev) => ({
              ...prev,
              numberOfLikes: parseInt(e.target.value),
            }))
          }
          labelProps={{ style: { marginBottom: 6 } }}
        />

        <Button
          size="xs"
          me={"auto"}
          onClick={() =>
            handleSubmit(() => {
              return BlogService.createBlog(blog as BlogCreate);
            }, "Thêm Thành Công")
          }
        >
          Xác Nhận
        </Button>
      </SimpleGrid>
    </DashboardLayout>
  );
}
