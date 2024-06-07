"use client";

import { Alert, Button, SimpleGrid, TextInput } from "@mantine/core";
import DashboardLayout from "../../../../components/Layout/DashboardLayout";
import {
  ICourseUpdate,
  CourseClient,
  CourseUpdate,
} from "../../../web-api-client";
import { handleSubmit } from "../../../../lib/helper";
import { useEffect, useState } from "react";
import useSWR from "swr";
import { SubjectSelect } from "../../../../components/Helper/AppSelect";

export default function CourseCreate({ params }: { params: { id: number } }) {
  const { id } = params;

  const CourseService = new CourseClient();

  const { data } = useSWR(
    `/api/course/${id}`,
    () => CourseService.getCourse(id),
    {
      revalidateIfStale: false,
      revalidateOnFocus: false,
      revalidateOnReconnect: false,
    }
  );

  const [course, setCourse] = useState<ICourseUpdate>({
    code: "",
    imageCourse: "",
    introduce: "",
    name: "",
    creatorId: 0,
    id: 0,
    numberOfPurchases: 0,
    numberOfStudent: 0,
    price: 0,
    totalCourseDuration: 0,
    courseSubjects: [],
  });

  useEffect(() => {
    if (data) setCourse(data);
  }, [data]);

  return (
    <DashboardLayout>
      {data ? (
        <SimpleGrid cols={{ base: 1, lg: 2 }}>
          <TextInput
            label="Tên Khóa Học"
            placeholder="Nhập Tên Khóa Học"
            value={course.name}
            onChange={(e) =>
              setCourse((prev) => ({ ...prev, name: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <TextInput
            label="Mã Khóa Học"
            placeholder="Nhập Mã Khóa Học"
            value={course.code}
            onChange={(e) =>
              setCourse((prev) => ({ ...prev, code: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <SubjectSelect
            val={course.courseSubjects}
            onChange={(e: []) =>
              setCourse((prev) => ({
                ...prev,
                courseSubjects: e,
              }))
            }
          />
          <TextInput
            label="Giới Thiệu"
            placeholder="Nhập Giới Thiệu"
            value={course.introduce}
            onChange={(e) =>
              setCourse((prev) => ({ ...prev, introduce: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <TextInput
            label="Ảnh Khóa Học"
            placeholder="Nhập Đường Dẫn Ảnh"
            value={course.imageCourse}
            onChange={(e) =>
              setCourse((prev) => ({ ...prev, imageCourse: e.target.value }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <TextInput
            label="Số Người Mua"
            placeholder="Nhập Số Người Mua"
            value={course.numberOfPurchases}
            onChange={(e) =>
              setCourse((prev) => ({
                ...prev,
                numberOfPurchases: Number(e.target.value),
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <TextInput
            label="Số Lượng Học Sinh"
            placeholder="Nhập Số Lượng Học Sinh"
            value={course.numberOfStudent}
            onChange={(e) =>
              setCourse((prev) => ({
                ...prev,
                numberOfStudent: Number(e.target.value),
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <TextInput
            label="Giá"
            placeholder="Nhập Giá"
            value={course.price}
            onChange={(e) =>
              setCourse((prev) => ({ ...prev, price: Number(e.target.value) }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <TextInput
            label="Tổng Thời Lượng Khóa Học"
            placeholder="Nhập Tổng Thời Lượng"
            value={course.totalCourseDuration}
            onChange={(e) =>
              setCourse((prev) => ({
                ...prev,
                totalCourseDuration: Number(e.target.value),
              }))
            }
            labelProps={{ style: { marginBottom: 6 } }}
          />
          <Button
            size="xs"
            ms={"auto"}
            onClick={() =>
              handleSubmit(() => {
                return CourseService.updateCourse(
                  course.id,
                  course as CourseUpdate
                );
              }, "Cập Nhật Thành Công")
            }
          >
            Xác Nhận
          </Button>
        </SimpleGrid>
      ) : (
        <Alert>Không Có Gì Ở Đây Cả</Alert>
      )}
    </DashboardLayout>
  );
}
