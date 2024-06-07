"use client";

import { Button, SimpleGrid, TextInput } from "@mantine/core";
import DashboardLayout from "../../../../components/Layout/DashboardLayout";
import { BlogClient, BlogCreate, IBlogCreate, IBlogUpdate } from "../../../web-api-client";
import { handleSubmit } from "../../../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";

export default function DashboardBlogUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  const BlogService = new BlogClient();

  const { data } = useSWR(`/api/course/${id}`, () => BlogService.getBlog(id), {
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
              return BlogService.updateBlog(blog.id, blog as BlogCreate);
            }, "Sửa Thành Công")
          }
        >
          Xác Nhận
        </Button>
      </SimpleGrid>
    </DashboardLayout>
  );
}
